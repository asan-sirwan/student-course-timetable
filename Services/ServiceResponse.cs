using System.Text.Json.Serialization;

namespace student_course_timetable.Services
{
	public class ServiceResponse<T>
	{
		[JsonIgnore]
		public T? Data { get; }

		[JsonIgnore]
		public bool IsSuccess { get; }

		public string? Error { get; }

		[JsonIgnore]
		public int StatusCode { get; }

		private ServiceResponse(bool isSuccess,	T? data, string? error, int statusCode)
		{
			IsSuccess = isSuccess;
			Data = data;
			Error = error;
			StatusCode = statusCode;
		}

		public static ServiceResponse<T> Success(T data, int statusCode) => new(true, data, null, statusCode);
		public static ServiceResponse<T> Fail(string error, int statusCode) => new(false, default, error, statusCode);
	}
}