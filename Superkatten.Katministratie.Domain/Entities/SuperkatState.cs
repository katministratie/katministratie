namespace Superkatten.Katministratie.Domain.Entities
{
    public enum SuperkatState
    {
        /// <summary>
        /// Gevangen en in de opvang geplaatst
        /// </summary>
        Trapped,

        /// <summary>
        /// Geneutraliseerd door de dierenarts
        /// </summary>
        Neutralized,

        /// <summary>
        /// Gereed om of te worden terug gezet of het adoptieprocess in te gaan
        /// </summary>
        Returnable,

        /// <summary>
        /// Katten nog onder controle
        /// </summary>
        Checked,

        /// <summary>
        ///  Katten zijn op hun plek en hoeven niet meer te worden gecontroleerd.
        /// </summary>
        Done
    }
}
