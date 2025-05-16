# InputStabilizer

Originally developed for stabilizing **sensor data**, `InputStabilizer<T>` filters out noise by only returning a value after it appears N times in a row.

```csharp
var s = new InputStabilizer<string>(3);
s.GetStableInput("open"); // null
s.GetStableInput("open"); // null
s.GetStableInput("open"); // "open"