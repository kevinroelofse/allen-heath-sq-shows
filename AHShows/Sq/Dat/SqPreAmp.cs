namespace AHShows.Sq.Dat;

public class SqPreAmp
{
    public int Gain { get; set; }
    public bool PhantomPower { get; set; }
    public bool Pad { get; set; }
    public int ChannelId { get; set; }
    
    public int GainEffective => Gain + (Pad ? -20 : 0);
}