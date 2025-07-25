using Godot;
using System;

namespace Scenes.Components.Actor
{
    public partial class LevelComponent : Node
    {
        [Export] public int Level { get; private set; } = 1;
        [Export] public int Experience { get; private set; } = 0;
        [Export] public int ExperienceToNextLevel { get; private set; } = 10;

        public void GainExperience(int amount)
        {
            Experience += amount;
            GD.Print($"Gained {amount} XP. Total: {Experience}");

            while (Experience >= ExperienceToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Experience -= ExperienceToNextLevel;
            Level++;
            ExperienceToNextLevel = (int)(ExperienceToNextLevel * 1.5f);
            GD.Print($" Level Up! Your Level {Level} (Next XP: {ExperienceToNextLevel})");
        }
    }
}
