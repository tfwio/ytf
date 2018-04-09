/*
 * User: xo
 * Date: 6/17/2017
 * Time: 11:33 PM
 */
using System;
using System.Collections.Generic;
using System.Data;
namespace TimeHelper
{
  interface ITimeInfo
  {
    int Days { get; set; }
    int Hour { get; set; }
    int Minute { get; set; }
    int second { get; set; }
    int frac { get; set; }
  }
  
  class Time : ITimeInfo
  {
    public int Days { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
    public double secondfrac { get; set; }
    public int second { get { return Convert.ToInt32(Math.Truncate(secondfrac)); } set { var frag = Convert.ToInt32(Math.Truncate(secondfrac)); secondfrac = frag-value; } }
    public int frac { get; set; }
  }
  
  [Obsolete]
  /// <summary>
  /// a experimentl Time Format (for the most part) rooted in TimeSpan's calcultions.
  /// 
  /// We are actually storing the data into a (double): <see cref="TotalMilliseconds"/>.
  /// 
  /// 
  /// Its strongly reccomended to look at how this is working given all of the possible exceptions when translating to/fro the various implicit operations provided.
  /// </summary>
  /// <remarks>
  /// timespan objects will throw an exception when...
  /// 
  /// A: minutes or seconds value exceeds 59 (range: 0-60 limit)
  /// 
  /// B: Hours exceed 23 (range: 0-23)
  /// 
  /// NOTE: any overflow would (should properly) be accumilted or appropriated into the proper Hour, Minute or (perhaps) Day value if we were to depend upon TimeSpan as we are.
  /// </remarks>
  public class TimeInfo
  {
    public TimeInfo() {  }
    public TimeInfo(string value){
      totalMilliseconds=value.StringToTimeSpan().TotalMilliseconds;
    }
    public TimeInfo(double ms){ totalMilliseconds =ms; }
    
    public static string Translate(string input)
    {
      string result = string.Empty;
      try { result = string.Format(TimeSpanStringFormatter.Self, "", input); }
      catch { System.Diagnostics.Debug.Print("Error parsing input TimeInfo."); result = Empty; }
      return result;
    }
    public override string ToString() { return StringValue; }

    public double TotalMilliseconds {
      get { return totalMilliseconds; }
      set { totalMilliseconds = value; }
    } double totalMilliseconds = 0d;
    
    public string StringValue {
      get { return Span.TimeSpanToString(); }
      //internal set { totalMilliseconds = value.StringToTimeSpan().TotalMilliseconds; }
    }
    
    /// to provide Hours, Minutes, Seconds, Etc...
    public TimeSpan Span { get { return TimeSpan.FromMilliseconds(totalMilliseconds); } }
    
    static public readonly string Empty = "0:00.00000";
    static public TimeInfo operator +(TimeInfo a, TimeInfo b) { return a.totalMilliseconds + b.totalMilliseconds; }
    static public TimeInfo operator -(TimeInfo a, TimeInfo b) { return a.totalMilliseconds - b.totalMilliseconds; }
    static public TimeInfo operator *(TimeInfo a, TimeInfo b) { return a.totalMilliseconds * b.totalMilliseconds; }
    static public TimeInfo operator /(TimeInfo a, TimeInfo b) { return a.totalMilliseconds / b.totalMilliseconds; }
    
    static public bool operator >(TimeInfo a, TimeInfo b) { return a.totalMilliseconds > b.totalMilliseconds; }
    static public bool operator <(TimeInfo a, TimeInfo b) { return a.totalMilliseconds < b.totalMilliseconds; }
    static public bool operator !=(TimeInfo a, TimeInfo b) { return a.totalMilliseconds != b.totalMilliseconds; }
    static public bool operator ==(TimeInfo a, TimeInfo b) { return a.totalMilliseconds == b.totalMilliseconds; }
    
    static public implicit operator TimeSpan(TimeInfo info) { return info.StringValue.StringToTimeSpan(); }
    static public implicit operator double(TimeInfo span) { return span.totalMilliseconds; }
    static public implicit operator long(TimeInfo span) { return (long)span.totalMilliseconds; }
    static public implicit operator int(TimeInfo span) { return (int)span.totalMilliseconds; }
    static public implicit operator string(TimeInfo span) { return span.StringValue; }
    
    static public implicit operator TimeInfo(TimeSpan span) { return new TimeInfo(span.TotalMilliseconds); }
    static public implicit operator TimeInfo(double span) { return new TimeInfo(span); }
    static public implicit operator TimeInfo(long span) { return new TimeInfo(span); }
    static public implicit operator TimeInfo(int span) { return new TimeInfo(span); }
    static public implicit operator TimeInfo(string span) { return new TimeInfo(span); }
    
  }
  
  public class TimeSpanStringFormatter : IFormatProvider, ICustomFormatter
  {
    internal static int TimeFormatPrecision = 5;
    internal static string SecondsTicks { get { return "{0:00."+"0".StringRepeat(TimeFormatPrecision)+"}"; } }
    internal static string StringFormat { get { return "{0:00}:{1:00}:{2}"; } }
    
    object IFormatProvider.GetFormat(Type formatType) { return this; }
    string ICustomFormatter.Format(string format, object arg, IFormatProvider formatProvider) { System.Diagnostics.Debug.Print("arg: {0}, Format: {1}", arg, format); return new TimeClass(arg as string).ToString(); }
    internal readonly static TimeSpanStringFormatter Self = new TimeSpanStringFormatter();
  }

  /// <summary>
  /// So that you can put in something like TimeInfo t = (SlopTime)"120:38.000" should theoretically result as == "2:00:38.000".
  /// </summary>
  public class TimeClass
  {
    static public readonly TimeClass Empty = new TimeClass(){ Milliseconds=0, Seconds=0, Minutes=0, Hours=0 };
    
    public double Hours        { get; set; } = 0;
    public double Minutes      { get; set; } = 0;
    public double Seconds      { get; set; } = 0;
    public double Milliseconds { get; set; } = 0;
    
    public double SecondsAndFrames { get { return Seconds + Milliseconds.DecimalFrames(decimal_precision); } set { Seconds = Math.Truncate(value); Milliseconds = (value - Seconds).IntFrames(decimal_precision); } }
    
    public double TotalMilliseconds { get { return GetTotalMilliseconds(); } set { SetTotalMilliseconds(value); } }
    public double TotalSeconds { get { return GetTotalSeconds(); } set { SetTotalSeconds(value); } }
    
    public long TotalMilliseconds_T { get { return (long)GetTotalMilliseconds(true); } }
    public long TotalSeconds_T { get { return (long)GetTotalSeconds(true); } }
    
    static int default_precision = 5;
    readonly int decimal_precision = default_precision;
    
    public TimeClass(TimeClass copy)
    {
      Milliseconds = copy.Milliseconds;
      Seconds      = copy.Seconds;
      Minutes      = copy.Minutes;
      Hours        = copy.Hours;
    }
    public TimeClass() : this("0") { }
    public TimeClass(string input) : this(input,default_precision) {}
    public TimeClass(string input, int framePrecision) {
      decimal_precision = framePrecision;
      ParseString(input, framePrecision);
    }
    
    static public TimeClass FromSeconds(double input)
    {
      var t = new TimeClass();
      t.TotalSeconds = input;
      return t;
    }
    
    static public TimeClass FromMilliseconds(double input)
    {
      var t = new TimeClass();
      t.TotalMilliseconds = input;
      return t;
    }
    
    public static void FromString(string input, ref TimeClass time, int precision=5) { time.ParseString(input,precision); }
    
    public void ParseString(string input, int precision=5)
    {
      Hours        = 0.0;
      Minutes      = 0.0;
      Seconds      = 0.0;
      Milliseconds = 0.0;
      
      var list = InitList(input);
      
      ParseList(list);
      //Rebalance();
      
      list.Clear();
      list = null;
    }
    
    /// Place values from a list to the proper "HH:MM:SS.fffff" slot
    void ParseList(IList<string> list)
    {
      double pMil=0, pMin=0, pHou=0;
      Seconds = list[2]
        .ForceParse()
        .T60(out pMin)
        .IntRemainder(out pMil, 5.0d);
      
      Minutes = (pMin+list[1].ForceParse())
        .T60(out pHou);
      
      Hours = (pHou+list[0].ForceParse());
      
      Milliseconds = pMil;
      Rebalance();
    }
    
    void Rebalance()
    {
      double rMin=0, rHou=0;
      Seconds = Seconds.T60(out rMin);
      Minutes = (rMin + Minutes).T60(out rHou);
      Hours = (rHou + Hours);
    }
    
    double GetTotalMilliseconds(bool truncate=false)
    {
      double seconds = truncate ? Math.Truncate(TotalSeconds.IntFrames(decimal_precision)) : TotalSeconds.IntFrames(decimal_precision);
      return seconds;
    }
    void SetTotalMilliseconds(double input)
    {
      Hours = 0;
      Minutes = 0;
      Seconds = 0;
      Milliseconds = 0;
      
      double seconds = input.DecimalFrames(decimal_precision), pMil=0;
      Seconds = seconds.IntRemainder(out pMil);
      Milliseconds = pMil;
      Minutes = Math.Truncate(seconds / 60);
      Hours   = Math.Truncate(seconds / 3600);
      //Seconds  = seconds % 60;
      Rebalance();
    }
    
    double GetTotalSeconds(bool truncate = false)
    {
      return truncate ? Math.Truncate((Hours * 3600)+(Minutes*60) + Seconds) : (Hours * 3600)+(Minutes*60) + Seconds;
    }
    void SetTotalSeconds(double input)
    {
      double pMil = 0;
      var seconds = input.IntRemainder(out pMil);
      Hours = 0;
      Minutes = 0;
      Seconds = input;
      Milliseconds = pMil;
      Rebalance();
    }
    
    public override string ToString() {
      return string.Format(
        "{0}:{1}:{2:00.00000}"/*.Replace("F5", "F"+decimal_precision.ToString())*/,
        Hours.ToUIntString(),
        Minutes.ToUIntString(2),
        SecondsAndFrames
       );
    }
    
    public static TimeClass FromString(string input) { var stf = new TimeClass(); FromString(input, ref stf); return stf; }
    
    static List<string> InitList(string input)
    {
      var values = new List<string>(input.Split(':'));
      if (values.Count==0) values.AddRange(new string[]{"00","00","00.00000"});
      if (values.Count==1) values.InsertRange(0,new string[]{"00","00"});
      if (values.Count==2) values.InsertRange(0,new string[]{"00"});
      return values;
    }
    static List<string> InitList(List<string> values)
    {
      if (values.Count==0) values.AddRange(new string[]{"00","00","00.00000"});
      if (values.Count==1) values.InsertRange(0,new string[]{"00","00"});
      if (values.Count==2) values.InsertRange(0,new string[]{"00"});
      return values;
    }
    
    static public implicit operator TimeClass(string input) { return new TimeClass(input); }
    static public implicit operator string(TimeClass input) { return input.ToString(); }
    
    static public TimeClass operator +(TimeClass a, TimeClass b) { return TimeClass.FromMilliseconds(a.TotalMilliseconds + b.TotalMilliseconds); }
    static public TimeClass operator -(TimeClass a, TimeClass b) { return TimeClass.FromMilliseconds(a.TotalMilliseconds - b.TotalMilliseconds); }
    static public TimeClass operator *(TimeClass a, TimeClass b) { return TimeClass.FromMilliseconds(a.TotalMilliseconds * b.TotalMilliseconds); }
    static public TimeClass operator /(TimeClass a, TimeClass b) { return TimeClass.FromMilliseconds(a.TotalMilliseconds / b.TotalMilliseconds); }
    
    static public bool operator  >(TimeClass a, TimeClass b) { return  a?.TotalMilliseconds > b?.TotalMilliseconds; }
    static public bool operator  <(TimeClass a, TimeClass b) { return  a?.TotalMilliseconds < b?.TotalMilliseconds; }
    static public bool operator !=(TimeClass a, TimeClass b) { return !a.TotalMilliseconds.Equals(b.TotalMilliseconds); }
    static public bool operator ==(TimeClass a, TimeClass b) { return (a ?? TimeClass.Empty).TotalMilliseconds.Equals((b ?? TimeClass.Empty).TotalMilliseconds); }
  }
  
  static public class TimeStringConversion
  {
    static public string ToUIntString(this double input, int lpad=0, int rpad=0)
    {
      var uv = (uint)input;
      var sv = uv.ToString();
      if (lpad > 0) sv = sv.PadLeft(lpad,'0');
      if (rpad > 0) sv = sv.PadRight(rpad,'0');
      return sv;
    }
    
    static public string PadEmptyInput(this List<string> list, int index, string EmptyValue="00")
    {
      var result = list[index].Trim();
      if (string.IsNullOrEmpty(result)) result = EmptyValue;
      if (result.Length==1) result = result.PadLeft(2,'0');
      return result;
    }
    static public string PadEmptyInput(this string list, string EmptyValue="00")
    {
      var result = list;
      if (string.IsNullOrEmpty(result)) result = EmptyValue;
      return result;
    }
    
    /// <summary>
    /// Truncates a time-related number such as seconds or minutes and returns the number of minutes or hours (depending on wether input is minutes or seconds).
    /// </summary>
    /// <param name="input">a number representing seconds or minutes.</param>
    /// <param name="mult">the resulting multiples of MAX will be returned to this memory.</param>
    /// <param name="max">[default=60] the MAXIUM threshold (numbers returned will be 0-(MAX-1).</param>
    /// <returns>The non-decimal portion of your input.</returns>
    static public double T60(this double input, out double mult, double max=60)
    {
      mult = Math.Truncate(input / max);
      return input % max;
    }
    
    /// <summary>
    /// The same as Math.Truncate, however we're yielding the decimal portion.
    /// </summary>
    static public double IntRemainder(this double input, out double decimalPart, double decimalPrecision=5)
    {
      var n = Math.Truncate(input);
      decimalPart = (input-n).IntFrames(decimalPrecision);
      return n;
    }
    
    /// return zero on fail.
    static public double ForceParse(this string input, double nullValue=0.0)
    {
      double result = nullValue;
      var hasresult = double.TryParse(input, out result);
      return hasresult ? result : nullValue;
    }
    
    /// <summary>
    /// If we input "23456" and specify a precision of "3", the result is "23.456".
    /// </summary>
    /// <param name="frames">our intput such as 0.1;  should always be less than 1 and contain a decimal portion.</param>
    /// <param name="precision">the number of zeros following the decimal point (on input).</param>
    static public double DecimalFrames(this double frames, double precision)
    {
      return frames * (1.0 / Math.Pow(10, precision));
    }
    /// <summary>
    /// convert a decimal number value (the decimal portion) to integer given precision exponential precision.
    /// 
    /// If we input "23.456" and specify a precision of "3", the result is "23456.0".
    /// </summary>
    static public double IntFrames(this double frames, double precision)
    {
      return frames * Math.Pow(10, precision);
    }
    
    static public double TotalHoursFromMinutes(this double input)
    {
      return (input - input.MinuteMod()) / 60.0;
    }
    static public double TotalMinutesFromSeconds(this double input)
    {
      return (input - input.MinuteMod()) / 60.0;
    }
    static public double MinuteMod(this double input)
    {
      return input % 60.0;
    }
    
    internal static string StringRepeat(this string to_repeat, int count)
    {
      int i = 0;
      string result = string.Empty;
      while (i < count) { result += to_repeat; i++; }
      return result;
    }
    
    static public string TimeSpanToString(this TimeSpan input)
    {
      return string.Format(
        TimeSpanStringFormatter.StringFormat,
        input.Hours,
        input.Minutes,
        string.Format(
          TimeSpanStringFormatter.SecondsTicks,
          input.Seconds + ((double)input.Milliseconds / 1000d))
       );
    }
    
    
    
    static public TimeSpan StringToTimeSpan(this string input)
    {
      string instr = string.Copy(input);
      var result = new TimeSpan(0);
      var t1 = input.Split('.');
      var t2 = t1[0].Split(':');
      var l1 = new List<string>(t1);
      var l2 = new List<string>(t2);
      if (string.IsNullOrEmpty(l1[0])) l1[0] = "00";
      
      switch (l1.Count)
      {
        case 0: // decimal-like value
          l1.Add("00");
          l1.Add("0".StringRepeat(TimeSpanStringFormatter.TimeFormatPrecision));
          break;
        case 1:
          l1.Add("0".StringRepeat(TimeSpanStringFormatter.TimeFormatPrecision));
          break;
      }
      switch (l2.Count)
      {
        case 0:
          l2.Insert(0,"00");
          l2.Insert(0,"00");
          l2.Insert(0,"00");
          break;
        case 1:
          l2.Insert(0,"00");
          l2.Insert(0,"00");
          break;
        case 2:
          l2.Insert(0,"00");
          break;
      }
      
      
      instr = string.Concat(string.Join(":",l2.ToArray()),".", l1[1]);
      
      bool success = TimeSpan.TryParse(instr, out result);
      if (!success) {
        System.Windows.Forms.MessageBox.Show(instr+"\n"+input,"failed: "+input);
        throw new ArgumentException("Failed parsing TimeInfo.");
      }
      return result;
    }
  }
}


