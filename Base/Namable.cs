﻿using System;

namespace Imagin.Core;
 
[Serializable]
public class Namable<T> : BaseNamable
{
    [Above, Horizontal, Index(-1)]
    public override string Name
    {
        get => base.Name;
        set => base.Name = value;
    }

    T value = default;
    public virtual T Value
    {
        get => value;
        set => this.Change(ref this.value, value);
    }

    public Namable() : base() { }

    public Namable(string name) : this() => Name = name;

    public Namable(string name, T value) : this(name) => Value = value;
}