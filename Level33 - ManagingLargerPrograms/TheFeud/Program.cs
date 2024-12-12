using IField;
using McDroid;
using McDroidPig = McDroid.Pig;
using IFieldPig = IField.Pig;
Sheep sheep = new Sheep();
Cow cow = new Cow();
/*McDroid.Pig pigMcDroid = new McDroid.Pig();
IField.Pig pigIField = new IField.Pig();*/
// OR
McDroidPig mcDroidPig = new McDroidPig();
IFieldPig iFieldPig = new IFieldPig();
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