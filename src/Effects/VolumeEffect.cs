﻿using System.Text;
using SoxSharp.Effects.Types;


namespace SoxSharp.Effects
{
  public class VolumeEffect : BaseEffect
  {
    public override string Name { get { return "vol"; } }

    public double Gain { get; set; }

    public GainType? Type { get; set; }

    public double? Limiter { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.VolumeEffect"/> class.
    /// </summary>
    /// <param name="gain">Volume gain.</param>
    public VolumeEffect(double gain)
    {
      Gain = gain;
    }


    public VolumeEffect(double gain, GainType type)
    : this(gain)
    {
      Type = type;
    }


    public VolumeEffect(double gain, GainType type, double limiter)
    : this(gain, type)
    {
      Limiter = limiter;
    }


    /// <summary>
    /// Translate a <see cref="T:SoxSharp.Effects.VolumeEffect"/> instance to a set of command arguments to be passed to SoX to be applied to the input file (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply a Volume effect.</returns>
    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder(Name);
      effectArgs.Append(" " + Gain);

      if (Type.HasValue)
      {
        switch (Type.Value)
        {
          case GainType.Amplitude:

            effectArgs.Append(" amplitude");
            break;

          case GainType.Power:

            effectArgs.Append(" power");
            break;

          case GainType.dB:

            effectArgs.Append(" dB");
            break;
        }
      }

      if (Limiter.HasValue)
        effectArgs.Append(" " + Limiter.Value);

      return effectArgs.ToString();
    }
  }
}