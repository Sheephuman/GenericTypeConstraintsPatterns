using System.ComponentModel;

namespace SheepWomanValidation.Entity;

public enum SheepAttribute
{
    [Description("ふわふわ羊耳（癒し系）")]
    FluffySheepEar,

    [Description("元気いっぱい仔羊（活発系）")]
    EnergeticLamb,

    [Description("おっとり黒羊（クール系）")]
    CalmBlackSheep,

    [Description("ツンデレ羊（ツン系）")]
    TsundereSheep,

    [Description("甘えん坊羊")]
    SpoiledLamb,

    [Description("賢い羊博士")]
    SheepScholar,

    [Description("照れ屋白羊")]
    ShyWhiteSheep,

    [Description("お姉さん羊")]
    BigSisterSheep,

    [Description("いたずら仔羊")]
    MischievousLamb,

    [Description("クールビューティー羊")]
    CoolBeautySheep,

    [Description("スポーツ羊")]
    SportsSheep,

    [Description("文学少女羊")]
    LiterarySheep,

    [Description("アイドル羊")]
    IdolSheep,

    [Description("メイド羊")]
    MaidSheep,

    [Description("冒険家羊")]
    AdventurerSheep,

    [Description("その他")]
    Other = 999
}

public enum AgeGroup
{
    [Description("20代")]
    Twenties,

    [Description("30代")]
    Thirties,

    [Description("40代")]
    Forties,

    [Description("50代")]
    Fifties
}

public enum PersonalityType
{
    [Description("従順")]
    Obedient,

    [Description("独立志向")]
    Independent
}
