using System;
using System.Collections.Generic;
using System.Text;

namespace OMX.Common
{
    public class ConstantValues
    {
        public const string MinimumPriceValue = "0";
        public const string MaximumPriceValue = "100000000";
        public const int MaximumTitleLength = 100;
        public const int MinimumTitleLength = 4;
        public const int DefaultBedBathRoomValue = 0;
        public const int MinimumDescriptionLength = 10;
        public const int MaximumDescriptionLength = 2000;
        public const string TitleErrorMessage = "The title must be between 4 and 10 symbols";
        public const string DescriptionErrorMessage = "The description must be between 10 and 2000 symbols";
    }
}
