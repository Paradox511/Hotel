﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICommand<T>: IRequest<Unit> where T : class
    {
        T Entity { get; }
    }
}
