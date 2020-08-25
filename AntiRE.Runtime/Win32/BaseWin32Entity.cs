using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using System.Web.Script.Serialization;

abstract class BaseWin32Entity
{
    public Dictionary<string, object> Properties { get; set; }
    public string Caption { get; protected set; }
    public string Name { get; protected set; }
    public string Manufacturer { get; protected set; }
    public string Model { get; protected set; }
    public string Description { get; protected set; }

    public BaseWin32Entity(ManagementBaseObject obj)
    {
        Properties = new Dictionary<string, object>();
        foreach (var p in obj.Properties)
            Properties.Add(p.Name, p.Value);

        Caption = ParseValue<string>(obj, "Caption");
        Name = ParseValue<string>(obj, "Name");
        Manufacturer = ParseValue<string>(obj, "Manufacturer");
        Model = ParseValue<string>(obj, "Model");
        Description = ParseValue<string>(obj, "Description");

        if (!string.IsNullOrEmpty(Caption))
            Caption = Caption.ToLower();

        if (!string.IsNullOrEmpty(Name))
            Name = Name.ToLower();

        if (!string.IsNullOrEmpty(Manufacturer))
            Manufacturer = Manufacturer.ToLower();

        if (!string.IsNullOrEmpty(Model))
            Model = Model.ToLower();

        if (!string.IsNullOrEmpty(Description))
            Description = Description.ToLower();
    }

    public override string ToString()
    {
        return string.Format("manufacturer={0}, name={1}, model={2}, caption={3}, description={4}"
            , Manufacturer
            , Name
            , Model
            , Caption
            , Description);
    }

    protected string PrintProperties()
    {
        var sb = new StringBuilder();
        foreach (var key in Properties.Keys)
            sb.AppendLine(string.Format("{0} = {1}", key, GetValue(Properties[key])));

        return sb.ToString();
    }

    object GetValue(object value)
    {
        var valueIsArray = false;

        if (value == null)
            return string.Empty;

        valueIsArray = value.GetType().IsArray;

        if (valueIsArray)
            return ToJSON(value);
        else
            return value;
    }

    protected string ToJSON(object value)
    {
        var js = new JavaScriptSerializer();
        return js.Serialize(value);
    }

    protected T ParseValue<T>(ManagementBaseObject obj, string key)
    {
        object value = null;
        try
        {
            value = obj[key];
        }
        catch { }

        if (value == null)
        {
            if (typeof(T) == typeof(string))
                return (T)Convert.ChangeType(string.Empty, typeof(T));
            else
                return default(T);
        }

        return (T)Convert.ChangeType(value, typeof(T));
    }
}
