using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private QuestDatabaseSo questDatabase;
        [SerializeField] private bool autoLoadOnAwake = true;
        [SerializeField] private bool autoSaveOnChange = true;

        public event Action<QuestRuntimeState> OnQuestAccepted;
        public event Action<QuestRuntimeState> OnQuestTracked;
        public event Action<QuestRuntimeState> OnQuestUpdated;
        public event Action<QuestRuntimeState> OnQuestCompleted;
        public event Action<QuestRuntimeState> OnQuestFailed;

        private readonly QuestBranchResolver _branchResolver = new();
        private readonly QuestRewardProcessor _rewardProcessor = new();
        private IQuestStateRepository _repository;

        public QuestJournalState Journal { get; private set; } = new();

        private void Awake()
        {
            _repository = new JsonQuestStateRepository();

            if (autoLoadOnAwake)
                LoadJournal();
        }

        public void RegisterRewardHandler(IQuestRewardHandler handler)
        {
            _rewardProcessor.RegisterHandler(handler);
        }

        public void LoadJournal()
        {
            Journal = _repository.Load() ?? new QuestJournalState();
        }

        public void SaveJournal()
        {
            _repository.Save(Journal);
        }

        public bool CanAcceptQuest(string questId)
        {
            var quest = questDatabase.GetQuestById(questId);
            if (quest == null) return false;
            if (Journal.GetQuestState(questId) != null) return false;

            foreach (var prerequisiteId in quest.prerequisiteQuestIds)
            {
                var prerequisiteState = Journal.GetQuestState(prerequisiteId);
                if (prerequisiteState == null || prerequisiteState.status != QuestStatus.Completed)
                    return false;
            }

            return true;
        }

        public bool AcceptQuest(string questId, bool autoTrack = true)
        {
            if (!CanAcceptQuest(questId))
                return false;

            var quest = questDatabase.GetQuestById(questId);
            var state = CreateQuestState(quest);

            Journal.acceptedQuests.Add(state);

            if (autoTrack)
                TrackQuest(questId);

            OnQuestAccepted?.Invoke(state);
            NotifyChanged();
            return true;
        }

        public void TrackQuest(string questId)
        {
            var state = Journal.GetQuestState(questId);
            if (state == null) return;

            foreach (var questState in Journal.acceptedQuests)
                questState.isTracked = false;

            state.isTracked = true;
            Journal.trackedQuestId = questId;

            OnQuestTracked?.Invoke(state);
            NotifyChanged();
        }

        public void HandleEvent(QuestEventPayload payload)
        {
            foreach (var questState in Journal.acceptedQuests)
            {
                if (questState.status != QuestStatus.Accepted)
                    continue;

                var quest = questDatabase.GetQuestById(questState.questId);
                if (quest == null)
                    continue;

                var currentNode = quest.GetNodeById(questState.currentNodeId);
                if (currentNode == null)
                    continue;

                if (!DoesEventMatchNode(currentNode, payload))
                {
                    TryResolveBranchWithoutProgress(quest, questState, currentNode, payload);
                    continue;
                }

                var nodeState = questState.GetNodeState(currentNode.nodeId);
                if (nodeState == null)
                    continue;

                nodeState.currentAmount += Mathf.Max(1, payload.amount);

                if (nodeState.currentAmount >= Mathf.Max(1, currentNode.requiredAmount))
                {
                    nodeState.currentAmount = currentNode.requiredAmount;
                    nodeState.isCompleted = true;
                    AdvanceQuest(quest, questState, currentNode, payload);
                }

                OnQuestUpdated?.Invoke(questState);
                NotifyChanged();
            }
        }

        private void TryResolveBranchWithoutProgress(QuestDefinitionSO quest, QuestRuntimeState questState, QuestNodeDefinition currentNode, QuestEventPayload payload)
        {
            var branch = _branchResolver.Resolve(currentNode, payload);
            if (branch == null) return;

            if (branch.failQuestIfMatched)
            {
                FailQuest(questState);
                return;
            }

            if (!string.IsNullOrWhiteSpace(branch.nextNodeId))
            {
                questState.branchLocked = true;
                questState.chosenBranchId = branch.branchId;
                MoveToNode(quest, questState, branch.nextNodeId);
                OnQuestUpdated?.Invoke(questState);
                NotifyChanged();
            }
        }

        private void AdvanceQuest(QuestDefinitionSO quest, QuestRuntimeState questState, QuestNodeDefinition currentNode, QuestEventPayload payload)
        {
            var branch = _branchResolver.Resolve(currentNode, payload);

            if (branch != null)
            {
                questState.branchLocked = true;
                questState.chosenBranchId = branch.branchId;

                if (branch.failQuestIfMatched)
                {
                    FailQuest(questState);
                    return;
                }

                MoveToNode(quest, questState, branch.nextNodeId);
                return;
            }

            if (currentNode.isTerminalNode || string.IsNullOrWhiteSpace(currentNode.nextNodeId))
            {
                CompleteQuest(quest, questState);
                return;
            }

            MoveToNode(quest, questState, currentNode.nextNodeId);
        }

        private void MoveToNode(QuestDefinitionSO quest, QuestRuntimeState questState, string nextNodeId)
        {
            if (string.IsNullOrWhiteSpace(nextNodeId))
            {
                CompleteQuest(quest, questState);
                return;
            }

            questState.currentNodeId = nextNodeId;

            var nextNodeState = questState.GetNodeState(nextNodeId);
            if (nextNodeState != null)
                nextNodeState.isRevealed = true;
        }

        private void CompleteQuest(QuestDefinitionSO quest, QuestRuntimeState questState)
        {
            questState.status = QuestStatus.Completed;

            if (!questState.rewardClaimed)
            {
                _rewardProcessor.ApplyRewards(quest);
                questState.rewardClaimed = true;
            }

            OnQuestCompleted?.Invoke(questState);
            NotifyChanged();
        }

        public void FailQuest(QuestRuntimeState questState)
        {
            questState.status = QuestStatus.Failed;
            OnQuestFailed?.Invoke(questState);
            NotifyChanged();
        }

        public QuestDefinitionSO GetQuestDefinition(string questId)
        {
            return questDatabase.GetQuestById(questId);
        }

        public IReadOnlyList<QuestRuntimeState> GetAcceptedQuests()
        {
            return Journal.acceptedQuests;
        }

        public QuestRuntimeState GetTrackedQuest()
        {
            return string.IsNullOrWhiteSpace(Journal.trackedQuestId)
                ? null
                : Journal.GetQuestState(Journal.trackedQuestId);
        }

        private QuestRuntimeState CreateQuestState(QuestDefinitionSO quest)
        {
            var state = new QuestRuntimeState
            {
                questId = quest.questId.Trim(),
                status = QuestStatus.Accepted,
                currentNodeId = quest.startNodeId.Trim()
            };

            foreach (var node in quest.nodes)
            {
                state.nodeStates.Add(new QuestNodeRuntimeState
                {
                    nodeId = node.nodeId.Trim(),
                    currentAmount = 0,
                    isCompleted = false,
                    isRevealed = node.nodeId.Trim() == quest.startNodeId.Trim()
                });
            }

            return state;
        }

        private bool DoesEventMatchNode(QuestNodeDefinition node, QuestEventPayload payload)
        {
            if (node == null || payload == null) return false;

            return node.nodeType switch
            {
                QuestNodeType.TalkToNpc => payload.eventType == QuestEventType.NpcTalked && payload.targetId == node.targetId,
                QuestNodeType.CollectItem => payload.eventType == QuestEventType.ItemCollected && payload.targetId == node.targetId,
                QuestNodeType.ReachLocation => payload.eventType == QuestEventType.LocationReached && payload.targetId == node.targetId,
                QuestNodeType.Interact => payload.eventType == QuestEventType.InteractionPerformed && payload.targetId == node.targetId,
                QuestNodeType.Custom => payload.targetId == node.targetId,
                _ => false
            };
        }

        private void NotifyChanged()
        {
            if (autoSaveOnChange)
                SaveJournal();
        }
    }
}