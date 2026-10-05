namespace AHShows.Sq.Dat.Parsing;

public class NvDataDatParser
{
    public List<SqPreAmp> ParseInputs(SqDatFile file)
    {
        List<SqPreAmp> baseInputs = new List<SqPreAmp>();
        int startOffset = 80029;
        for (int i = 0; i < 48; i++)
        {
            SqPreAmp input = ParseInput(file.Data, i+1, startOffset + (i * 4));
            baseInputs.Add(input);
        } 
        
        
        return baseInputs;
    }

    private SqPreAmp ParseInput(byte[] fileData, int channelId, int startOffset)
    {
        SqPreAmp preAmp = new SqPreAmp();
        preAmp.ChannelId = channelId;

        byte phantomPower = fileData[startOffset + 1];
        preAmp.PhantomPower = Convert.ToBoolean(phantomPower);
        byte pad = fileData[startOffset + 2];
        preAmp.Pad = Convert.ToBoolean(pad);
        byte gain = fileData[startOffset];
        preAmp.Gain = Convert.ToInt32(gain) - 128;
        return preAmp;
    }
}