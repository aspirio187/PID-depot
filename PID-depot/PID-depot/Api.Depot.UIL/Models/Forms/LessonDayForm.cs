using System;

namespace Api.Depot.UIL.Models.Forms
{
    public class LessonDayForm
    {
        public int Day { get; set; }
        public string DayName { get; set; }
        public bool IsSelected { get; set; }
        // TODO : Ajouter un attribut qui vérifie si l'heure de début est plus petite que l'heure de fin
        // TODO : Ajouter un attribut qui empêche les heures de début et de fin d'être en dessous de 8h ou 18h
        public TimeSpan StartsAt { get; set; }
        public TimeSpan EndsAt { get; set; }
    }
}
