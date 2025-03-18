// Auto-generated file. Do not modify manually.

namespace FFXIVConnector.Network.Models
{
    public enum ServerZoneIpcType : ushort
    {
        AirshipExplorationResult = 258,
        ActorControlSelf = 396,
        ActorMove = 796,
        ActorCast = 974,
        ActorGauge = 355,
        AirshipStatus = 312,
        ActorSetPos = 547,
        ActorControlTarget = 277,
        AirshipStatusList = 839,
        ActorControl = 910,
        AirshipTimers = 782,
        ContainerInfo = 746,
        CurrencyCrystalInfo = 399,
        CFNotify = 786,
        CraftingLog = 110,
        DesynthResult = 423,
        EventStart = 436,
        EventPlay8 = 727,
        Effect = 113,
        EnvironmentControl = 960,
        EventPlay32 = 539,
        EventPlay = 133,
        EventPlay16 = 721,
        EffectResultBasic = 670,
        EventPlay4 = 653,
        EventFinish = 842,
        EventPlay64 = 768,
        EventPlay255 = 629,
        Examine = 756,
        ExamineSearchInfo = 211,
        EventPlay128 = 196,
        EffectResult = 911,
        FreeCompanyInfo = 517,
        FreeCompanyDialog = 357,
        GatheringLog = 874,
        InitZone = 785,
        InventoryTransaction = 807,
        InventoryActionAck = 752,
        IslandWorkshopSupplyDemand = 775,
        InventoryTransactionFinish = 325,
        ItemInfo = 938,
        ItemMarketBoardInfo = 434,
        Logout = 873,
        MarketBoardItemListingHistory = 770,
        MarketBoardItemListing = 182,
        MarketBoardPurchase = 167,
        MarketBoardItemListingCount = 971,
        MarketBoardSearchResult = 975,
        NpcSpawn = 256,
        ObjectSpawn = 295,
        PlayerStats = 506,
        PlayerSetup = 107,
        PrepareZoning = 521,
        PlayerSpawn = 427,
        PlaceFieldMarkerPreset = 373,
        PlaceFieldMarker = 203,
        Playtime = 228,
        ResultDialog = 556,
        RetainerInformation = 838,
        SubmarineProgressionStatus = 651,
        StatusEffectList3 = 121,
        StatusEffectList = 187,
        SubmarineTimers = 276,
        SubmarineExplorationResult = 739,
        StatusEffectList2 = 369,
        SystemLogMessage = 397,
        SubmarineStatusList = 562,
        UpdateInventorySlot = 257,
        UpdateHpMpTp = 168,
        UpdateSearchInfo = 411,
        UpdateClassInfo = 106,
        WeatherChange = 272,
    }

    public enum ClientZoneIpcType : ushort
    {
        InventoryModifyHandler = 220,
        MarketBoardPurchaseHandler = 561,
        SetSearchInfoHandler = 946,
        UpdatePositionHandler = 585,
    }

    public enum ServerLobbyIpcType : ushort
    {
        LobbyError = 2,
        LobbyServiceAccountList = 12,
        LobbyCharList = 13,
        LobbyCharCreate = 14,
        LobbyEnterWorld = 15,
        LobbyServerList = 21,
        LobbyRetainerList = 23,
    }

    public enum ClientLobbyIpcType : ushort
    {
        ClientVersionInfo = 5,
        ReqCharList = 3,
        ReqEnterWorld = 4,
        ReqCharDelete = 10,
        ReqCharCreate = 11,
    }

}
