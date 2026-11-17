using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay.States
{
    /// <summary>
    /// конкретное состояние: ученик отчислен
    /// </summary>
    internal class ExpelledState : IStudentState
    {
        public string StatusName => "Отчислен";

        public bool CanTransitionTo(IStudentState newState) => newState is EnrolledState; // можно восстановить в "Обучается"
    }
}
