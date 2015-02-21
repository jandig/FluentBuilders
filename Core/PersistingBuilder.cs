﻿using System;

namespace BuildBuddy.Core
{
    public abstract class PersistingBuilder<TSubject> : Builder<TSubject>, IPersistingBuilder
        where TSubject : class
    {
        public bool Persist { get; set; }

        protected PersistingBuilder(IBuilderManager mgr) : base(mgr)
        {
        }

        public new PersistingBuilder<TSubject> Customize(Action<TSubject> action)
        {
            Customizations.Add(action);
            return this;
        }

        public override TSubject Create(int seed = 0)
        {
            var subject = base.Create(seed);
            if (Persist)
            {
                Save(subject);
                PostPersist(subject);
            }

            return subject;
        }

        protected abstract void Save(TSubject subject);

        protected virtual void PostPersist(TSubject subject)
        {
        }
    }
}