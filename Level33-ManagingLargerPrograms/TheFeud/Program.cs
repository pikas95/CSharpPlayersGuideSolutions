using IField;
using McDroid;
using Pig = McDroid.Pig;

Sheep sheep = new Sheep();
Cow cow = new Cow();

IField.Pig pigIField = new IField.Pig();
Pig pigMcDroid = new Pig();

namespace IField
{
    public class Sheep { }
    public class Pig { }
}

namespace McDroid
{
    public class Cow { }
    public class Pig { }
}