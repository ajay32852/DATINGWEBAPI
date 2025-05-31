using System.Text.Json;
public class ResponseData
{
    public int Code { get; set; }
    public bool Status { get; set; }
    public object Message { get; set; }
    public object? Data { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

