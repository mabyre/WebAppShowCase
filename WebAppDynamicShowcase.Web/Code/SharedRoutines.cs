using System.Data;
using System;
using System.Web;
using System.Text;

public class SharedRoutines
{
    const int TRUNCATE_COUNT = 50;

    public static string truncate( string originalInput )
    {
        return truncate( originalInput, TRUNCATE_COUNT );
    }

    public static string truncate( string originalInput, int wordsLimit )
    {
        if ( ( originalInput != null ) && ( originalInput != "" ) && ( originalInput != System.Convert.DBNull.ToString() ) )
        {
            StringBuilder output = new StringBuilder( originalInput.Length );
            StringBuilder input = new StringBuilder( originalInput );
            int words = 0;
            bool lastwasWS = true;
            int count = 0;
            do
            {
                if ( char.IsWhiteSpace( input.ToString(), count ) )
                {
                    lastwasWS = true;
                }
                else
                {
                    if ( lastwasWS ) words += 1;
                    lastwasWS = false;
                }
                output.Append( input.ToString( count, 1 ) );
                count += 1;
            }
            while ( ( words < wordsLimit || lastwasWS == false ) && count < ( originalInput.Length ) );

            return output.ToString();
        }
        else
        {
            return "-";
        }
    }

    public string Encode( object contents )
    {
        return HttpUtility.HtmlEncode( ( string )contents );
    }

    public string encodeAndTruncate( object contents )
    {
        return truncate( Encode( contents ) );
    }

    public static string GetFriendlyDate( System.DateTime src, bool showtime )
    {
        string str;
        DateTime currdate = DateTime.Now;
        int datediff = ( src - currdate ).Days;
        switch ( datediff )
        {
            case -1:
                str = "Yesterday at ";
                break;
            case -7: // TODO: to -2
                str = "Last " + src.DayOfWeek.ToString();
                break;
            case 0:
                str = "Today ";
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
                str = "This " + src.DayOfWeek.ToString();
                break;
            default:
                str = src.ToShortDateString();
                break;
        }
        if ( showtime == true )
        {
            str += " at " + src.ToShortTimeString();
        }
        return str;
    }

    public static string GetFriendlyDate( DateTime src )
    {
        string str;
        DateTime currdate = DateTime.Now;
        int datediff = ( src - currdate ).Days;
        switch ( datediff )
        {
            case -1:
                str = "Yesterday at ";
                break;
            case -7: // TODO: to -2
                str = "Last " + src.DayOfWeek.ToString();
                break;
            case 0:
                str = "Today ";
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
                str = "This " + src.DayOfWeek.ToString();
                break;
            default:
                str = src.ToShortDateString();
                break;
        }
        return str;
    }

    public static int NumPages( int RowCount )
    {
        int count = RowCount;

        int pagesize = 10; // ClubStarterKit.Web.Settings.PageSize();

        double dbl = count / 5;
        double rounded = Math.Ceiling( dbl );
        int testint = ( int )rounded;

        if ( testint > 0 )
        {
            return testint;
        }
        else
        {
            if ( count > 0 )
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}

