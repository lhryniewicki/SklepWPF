using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SklepWPF.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum PaymentMethod
    {
        [Description("Kartą")]
        ByCard,
        [Description("Gotówką")]
        ByCash
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum MessageDisplay
    {
        [Description("Wysłane")]
        Sent,
        [Description("Odebrane")]
        Received
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum OrderStatus
    {
        [Description("W toku")]
        Pending,
        [Description("Zrealizowane")]
        Completed,
        [Description("Anulowane")]
        Cancelled
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ClientPanelDisplayedItems
    {
        [Description("Historia zamówień")]
        Orders,
        [Description("Obserwowane produkty")]
        ObservedProducts
    }
}
