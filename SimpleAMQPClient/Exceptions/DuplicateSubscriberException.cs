using System;

public class DuplicateSubscriberException : Exception {
    public DuplicateSubscriberException() { }

    public DuplicateSubscriberException(string message) : base(message) { }

    public DuplicateSubscriberException(string message, Exception inner) : base(message, inner) { }
}