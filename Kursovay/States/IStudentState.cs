using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay.States
{
    /// <summary>
    /// интерфейс, описывающий состояние ученика
    /// </summary>
    internal interface IStudentState
    {
        string StatusName { get; }
        bool CanTransitionTo(IStudentState newState);
    }
}
