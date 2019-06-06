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
        ByCash,
        [Description("Przelew")]
        ByBank,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum DeliveryMethod
    {
        [Description("Pocztex")]
        ByPocztex,
        [Description("Odbiór osobisty")]
        ByYourself,
        [Description("Paczkomat")]
        ByInpost,
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

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ProductListSortingProducts
    {
        [Description("Bez sortowania")]
        WithoutSort,
        [Description("Ronąco wg nazwy")]
        NameAsc,
        [Description("Malejąco wg nazwy")]
        NameDesc,
        [Description("Ronąco wg ceny")]
        PrizeAsc,
        [Description("Malejąco wg ceny")]
        PrizeDesc,
        [Description("Rosnąco wg ilosci")]
        QuantityAsc,
        [Description("Malejąco wg ilosci")]
        QunatityDesc
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ClientListSortingClients
    {
        [Description("Bez sortowania")]
        WithoutSort,
        [Description("Ronąco wg nazwy")]
        NicknameAsc,
        [Description("Malejąco wg nazwy")]
        NicknameDesc,
        [Description("Ronąco wg imienia")]
        NameAsc,
        [Description("Malejąco wg imienia")]
        NameDesc,
        [Description("Rosnąco wg nazwiska")]
        SurnameAsc,
        [Description("Malejąco wg nazwiska")]
        SurnameDesc,
        [Description("Rosnąco wg emaila")]
        EmailAsc,
        [Description("Malejąco wg emaila")]
        EmailDesc
    }
}
