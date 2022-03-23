using Api.Depot.UIL.ValidationAttributes;
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
        [TimeSpanDelimitation(8, 0, 18, 0,
            ErrorMessage = "Les cours ne peuvent commencer qu'entre 8:00 et 18:00 compris !")]
        public TimeSpan StartsAt { get; set; }

        [TimeSpanDelimitation(8, 0, 18, 0,
            ErrorMessage = "Les cours ne peuvent se terminer qu'entre 8:00 et 18:00 compris !")]
        [TimeSpanComparison(nameof(StartsAt),
            ErrorMessage = "La fin du cours ne peut pas précéder ou égaler le début du cours !")]
        public TimeSpan EndsAt { get; set; }
    }
}
