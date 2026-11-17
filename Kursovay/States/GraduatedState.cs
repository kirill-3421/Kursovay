using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay.States
{
    /// <summary>
    /// конкретное состояние: ученик выпущен
    /// </summary>
    internal class GraduatedState : IStudentState
    {
        public string StatusName => "Выпущен";

        public bool CanTransitionTo(IStudentState newState) => false; // из "Выпущен" никуда нельзя
    }
}
