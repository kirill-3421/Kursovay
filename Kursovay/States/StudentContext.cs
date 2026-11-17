using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay.States
{
    /// <summary>
    /// создает объект состояния ученика, в зависимости от статуса ученика в БД создает подходящий класс состояния
    /// </summary>
    internal class StudentContext
    {
        public IStudentState State { get; private set; }

        public StudentContext(string initialStatus)
        {
            State = initialStatus switch
            {
                "Выпущен" => new GraduatedState(),
                "Отчислен" => new ExpelledState(),
                _ => new EnrolledState()
            };
        }

        public bool TryChangeStatus(IStudentState newState)
        {
            if (State.CanTransitionTo(newState))
            {
                State = newState;
                return true;
            }
            return false;
        }
    }
}
