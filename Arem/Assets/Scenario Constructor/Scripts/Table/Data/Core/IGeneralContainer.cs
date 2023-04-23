using System;
using System.Collections.Generic;

public interface IGeneralContainer
{
    public Dictionary<Type, IGeneralizable> Content { get; set; }
    public T GetContent<T>() where T : IGeneralizable;
}