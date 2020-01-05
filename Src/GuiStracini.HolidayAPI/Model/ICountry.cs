namespace GuiStracini.HolidayAPI.Model
{
    public interface ICountry
    {
        string Code { get; }
        string Name { get; }
        string[] Languages { get; }
        CountryCode Codes { get; }
    }
}
