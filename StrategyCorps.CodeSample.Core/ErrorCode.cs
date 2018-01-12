using System.ComponentModel;

namespace StrategyCorps.CodeSample.Core
{
    public enum ErrorCode
    {

        //These descriptions are intended to be friendly messages. Do NOT write a message you do not want a user to see, or that a user will not understand. (ie. MemberInformationModel is required. A user does not know what MemberInformationModel is, nor how to provide one. This is a message that should be left in the 'developerMessage' parameter of the returning ErrorResponse call.

        [Description("We're sorry there was a problem on our end please try again.")]
        Default = 0,

        [Description("We're sorry there was a problem on our end please try again.")]
        Unknown = 999
    }
}
