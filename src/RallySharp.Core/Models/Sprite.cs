﻿using System.Text.Json.Serialization;

namespace RallySharp.Models
{
    public abstract class Sprite
    {
        public Vec Pos { get; set; }

        // animation
        public abstract int CurrentFrame { get; }

        protected virtual void UpdateStart()
        {
        }

        protected virtual void UpdateReady()
        {
        }

        protected virtual void UpdateRunning()
        {
        }

        protected virtual void UpdateCrashed()
        {
        }

        protected virtual void UpdateCompleted()
        {
        }

        protected virtual void UpdateFinished()
        {
        }

        [JsonIgnore]
        public Action Update { get; protected set; }

        public Sprite Start()
        {
            Update = UpdateStart;
            return this;
        }

        public Sprite Ready()
        {
            Update = UpdateReady;
            return this;
        }

        public Sprite Running()
        {
            Update = UpdateRunning;
            return this;
        }

        public Sprite Crashed()
        {
            Update = UpdateCrashed;
            return this;
        }

        public Sprite Finished()
        {
            Update = UpdateFinished;
            return this;
        }

        public Sprite Completed()
        {
            Update = UpdateCompleted;
            return this;
        }
    }
}
