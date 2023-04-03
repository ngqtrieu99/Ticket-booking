using System.Text.RegularExpressions;

namespace TicketBooking.Common.AppExceptions
{
    public enum SeatClassType
    {
        Economy,
        Business
    };
    
    public interface IFlightValidation
    {
        public bool FlightDateValid(DateTime departdate, DateTime arrivaldate);
        public bool AirportNameValid(string airportname);
        public string AirportNameProcess(string airportname);
    }
    
    public class FlightValidation : IFlightValidation
    {
        private readonly List<string> airportCode = new List<string>()
        {
            "SGN", "UIH", "HAN"
        };
        public bool FlightDateValid(DateTime departdate, DateTime arrivaldate)
        {
            // Validate for flight POST
            // Case 1: Flight arrival day is earlier than departure day -> false 
            // Case 2: Flight arrival day is not earlier than departure day
            //         but the flight time is less than 1 hour -> false
            return arrivaldate.Date > departdate.Date
                   || ((arrivaldate.Date == departdate.Date)
                       && (arrivaldate.Hour - departdate.Hour) >= 1);
        }
        
        public bool AirportNameValid(string airportname)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            return airportname.Length == 3 && regexItem.IsMatch(airportname) && airportCode.Contains(airportname);
        }

        public string AirportNameProcess(string airportname)
        {
            // Suppose all case has passed,
            // the last step to to convert all lowercase to uppercase (if there is any)
            airportname = airportname.ToUpper();
            return airportname;
        }
    }
}

