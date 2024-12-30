using System.Xml.Serialization;

namespace BGGMarketPlaceStats.Infrastructure.BGG;

public class BggXmlApi2Response
{
    [XmlRoot("items")]
    public class Items
    {
        [XmlAttribute("termsofuse")] public required string TermsOfUse { get; set; }

        [XmlElement("item")] public required List<Item> ItemList { get; set; }
    }

    public class Item
    {
        [XmlAttribute("type")] public required string Type { get; set; }

        [XmlAttribute("id")] public int Id { get; set; }

        [XmlElement("thumbnail")] public required string Thumbnail { get; set; }

        [XmlElement("image")] public required string Image { get; set; }

        [XmlElement("name")] public required List<Name> Names { get; set; }

        [XmlElement("description")] public required string Description { get; set; }

        [XmlElement("yearpublished")] public required ValueElement YearPublished { get; set; }

        [XmlElement("minplayers")] public required ValueElement MinPlayers { get; set; }

        [XmlElement("maxplayers")] public required ValueElement MaxPlayers { get; set; }

        [XmlElement("poll")] public required List<Poll> Polls { get; set; }

        [XmlElement("poll-summary")] public required List<PollSummary> PollSummaries { get; set; }

        [XmlElement("playingtime")] public required ValueElement PlayingTime { get; set; }

        [XmlElement("minplaytime")] public required ValueElement MinPlayTime { get; set; }

        [XmlElement("maxplaytime")] public required ValueElement MaxPlayTime { get; set; }

        [XmlElement("minage")] public required ValueElement MinAge { get; set; }

        [XmlElement("link")] public required List<Link> Links { get; set; }

        [XmlElement("marketplacelistings")] public required MarketplaceListings MarketplaceListings { get; set; }
    }

    public class Name
    {
        [XmlAttribute("type")] public required string Type { get; set; }

        [XmlAttribute("sortindex")] public int SortIndex { get; set; }

        [XmlAttribute("value")] public required string Value { get; set; }
    }

    public class ValueElement
    {
        [XmlAttribute("value")] public required string Value { get; set; }
    }

    public class Poll
    {
        [XmlAttribute("name")] public required string Name { get; set; }

        [XmlAttribute("title")] public required string Title { get; set; }

        [XmlAttribute("totalvotes")] public int TotalVotes { get; set; }

        [XmlElement("results")] public required List<Results> Results { get; set; }
    }

    public class Results
    {
        [XmlAttribute("numplayers")] public required string NumPlayers { get; set; }

        [XmlElement("result")] public required List<Result> ResultList { get; set; }
    }

    public class Result
    {
        [XmlAttribute("value")] public required string Value { get; set; }

        [XmlAttribute("numvotes")] public int NumVotes { get; set; }

        [XmlAttribute("level")] public int Level { get; set; }
    }

    public class PollSummary
    {
        [XmlAttribute("name")] public required string Name { get; set; }

        [XmlAttribute("title")] public required string Title { get; set; }

        [XmlElement("result")] public required List<PollSummaryResult> Results { get; set; }
    }

    public class PollSummaryResult
    {
        [XmlAttribute("name")] public required string Name { get; set; }

        [XmlAttribute("value")] public required string Value { get; set; }
    }

    public class Link
    {
        [XmlAttribute("type")] public required string Type { get; set; }

        [XmlAttribute("id")] public int Id { get; set; }

        [XmlAttribute("value")] public required string Value { get; set; }
    }

    public class MarketplaceListings
    {
        [XmlElement("listing")] public required List<Listing> Listings { get; set; }
    }

    public class Listing
    {
        [XmlElement("listdate")] public required ValueElement ListDate { get; set; }

        [XmlElement("price")] public required Price Price { get; set; }

        [XmlElement("condition")] public required ValueElement Condition { get; set; }

        [XmlElement("notes")] public required ValueElement Notes { get; set; }

        [XmlElement("link")] public required Link Link { get; set; }
    }

    public class Price
    {
        [XmlAttribute("currency")] public required string Currency { get; set; }

        [XmlAttribute("value")] public decimal Value { get; set; }
    }
}