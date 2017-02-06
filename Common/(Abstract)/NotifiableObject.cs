﻿using System;
using System.ComponentModel;
using System.Timers;
using System.Xml.Serialization;

namespace Imagin.Common
{
    /// <summary>
    /// An object capable of raising notifications using <see cref="System.Timers.Timer"/>.
    /// </summary>
    [Serializable]
    public class NotifiableObject : AbstractObject, INotifiable
    {
        /// <summary>
        /// The default interval to use.
        /// </summary>
        [field: NonSerialized]
        public const double DefaultInterval = 1000d;

        /// <summary>
        /// Occurs when <see cref="Timer"/> elapses.
        /// </summary>
        [field:NonSerialized]
        public event ElapsedEventHandler Notified;

        /// <summary>
        /// A timer used for automating notifications.
        /// </summary>
        [XmlIgnore]
        protected Timer Timer
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets whether or not to enable notifications.
        /// </summary>
        [XmlIgnore]
        public virtual bool Enabled
        {
            get
            {
                return Timer.Enabled;
            }
            set
            {
                Timer.Enabled = value;
                OnPropertyChanged("Enabled");
            }
        }

        /// <summary>
        /// The period of time (in milliseconds) between notifications.
        /// </summary>
        [XmlIgnore]
        public virtual double Interval
        {
            get
            {
                return Timer.Interval;
            }
            set
            {
                Timer.Interval = value;
                OnPropertyChanged("Interval");
            }
        }

        /// <summary>
        /// Occurs when <see cref="Timer"/> elapses.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnNotified(ElapsedEventArgs e)
        {
            Notified?.Invoke(this, e);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifiableObject"/> class and enables (or disables) timer with given interval.
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="enabled"></param>
        public NotifiableObject(double interval = DefaultInterval, bool enabled = false) : base()
        {
            Timer = new Timer();
            Timer.Elapsed += (s, e) => OnNotified(e);

            Interval = interval;
            Enabled = enabled;
        }
    }
}
