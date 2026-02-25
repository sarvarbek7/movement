using Movement.Application.Requests;

namespace Movement.Application.Test.Queries;

public class TestQuery(string pinfl) : PinfCheckableQuery<string>(pinfl);