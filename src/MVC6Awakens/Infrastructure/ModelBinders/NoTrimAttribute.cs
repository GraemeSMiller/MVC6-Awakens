﻿using System;


namespace MVC6Awakens.Infrastructure.ModelBinders
{
    /// <summary>
    /// NoTrim Attribute is used on View model fields that must not be trimmed of the witespace.
    /// This works in pair with <see cref="TrimmingSimpleTypeModelBinder"/>, where by default all whitespace from POST strings is trimmed,
    /// but sometimes (like password) you don't want trimming.
    /// </summary>
    public class NoTrimAttribute : Attribute
    {
        // nothing here. Just a decoration
    }
}