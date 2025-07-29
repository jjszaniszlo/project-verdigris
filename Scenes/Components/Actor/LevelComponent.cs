using Godot;
using System;

namespace Scenes.Components.Actor
{
    public partial class LevelComponent : Node
    {
        [Export] public int Level { get; private set; } = 1;
        [Export] public int Experience { get; private set; } = 0;
        [Export] public int ExperienceToNextLevel { get; private set; } = 10;

        [Signal]
        public delegate void LevelChangedEventHandler(int levelsGained, int experience, int ExperienceToNextLevel);

        private int _OldLevel = 1;

        public void GainExperience(int amount)
        {
            Experience += amount;

            _OldLevel = Level;
            while (Experience >= ExperienceToNextLevel)
            {
                LevelUp();
            }
            int levelsGained = Math.Max(0, Level - _OldLevel);
            EmitSignal(SignalName.LevelChanged, levelsGained, Experience, ExperienceToNextLevel);
        }

        private void LevelUp()
        {
            Experience -= ExperienceToNextLevel;
            Level++;
            ExperienceToNextLevel = (int)(ExperienceToNextLevel * 1.5f);
            GD.Print($"Level up! New level: {Level}, Experience to next level: {ExperienceToNextLevel}");
        }
    }
}
