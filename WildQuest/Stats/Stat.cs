namespace WildQuest.Stats;

[System.Serializable]
public class Stat 
{         
   protected int _currentValue;
   protected int _maxValue;
   
   public virtual int CurrentValue
   {
       get => _currentValue;
	   set => _currentValue = int.Clamp(value, 0, _maxValue);
   }
   
   public virtual int MaxValue
   {
       get => _maxValue;
	   set
	   {
           _maxValue = value;
		   _currentValue = _maxValue;
       }
   }
   
   public Stat(int maxValue)
   {	
	   MaxValue = maxValue;
   }
   
   public Stat(int currentValue, int maxValue)
   {
	   _currentValue = currentValue;
	   _maxValue = maxValue;
   }
   
   public static Stat operator +(Stat a, Stat b)
   {
	   return new Stat(a.CurrentValue + b.CurrentValue, a.MaxValue + b.MaxValue);
   }
   
   public static Stat operator -(Stat a, Stat b)
   {
	   return new Stat(a.CurrentValue - b.CurrentValue, a.MaxValue - b.MaxValue);
   }
   
   public static Stat operator *(Stat a, Stat b)
   {
	   return new Stat(a.CurrentValue * b.CurrentValue, a.MaxValue * b.MaxValue);
   }
   
   public static Stat operator /(Stat a, Stat b)
   {
	   return new Stat(a.CurrentValue / b.CurrentValue, a.MaxValue / b.MaxValue);
   }
   
}
