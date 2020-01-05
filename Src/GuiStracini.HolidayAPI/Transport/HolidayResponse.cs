namespace GuiStracini.HolidayAPI.Transport
{
    using Model;
    using System.Collections.Generic;

    public class HolidayResponse : BaseResponse
    {
        public IEnumerable<Holiday> Holidays { get; set; }
    }
}
