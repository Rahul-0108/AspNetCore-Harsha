using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ResultFilters
{
// for IAlwaysRunResultFilter both OnResultExecuting and   OnResultExecuted always executed even if previous filters are short circuited
 public class PersonAlwaysRunResultFilter : IAlwaysRunResultFilter
 {
  public void OnResultExecuted(ResultExecutedContext context)
  {
  }

  public void OnResultExecuting(ResultExecutingContext context)
  {
  }
 }
}
