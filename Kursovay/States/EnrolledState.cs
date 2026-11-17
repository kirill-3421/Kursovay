using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay.States
{
    /// <summary>
    /// конкретное состояние: ученик обучается
    /// </summary>
    internal class EnrolledState : IStudentState
    {
        public string StatusName => "Обучается";

        public bool CanTransitionTo(IStudentState newState) => true; // из "Обучается" можно в любое состояние
    }
}
