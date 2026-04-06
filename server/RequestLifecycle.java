import com.sun.net.httpserver.HttpExchange;
import java.nio.charset.StandardCharsets;
import java.io.*;
import java.lang.reflect.InvocationTargetException;

import com.fasterxml.jackson.core.*;
import com.fasterxml.jackson.databind.ObjectMapper;

public abstract class RequestLifecycle<E extends Record> {
    protected HttpExchange hx;
    private Class<E> recordType;
    private String allowMethod;

    private final static ObjectMapper JSONMapper = new ObjectMapper();

    public RequestLifecycle(HttpExchange hx, Class<E> type, String allowMethod) {
        this.hx = hx;
        this.recordType = type;
        this.allowMethod = allowMethod;
    }

    public void handle() throws IOException {
        try {
            String method = this.hx.getRequestMethod();

            if (this.allowMethod.equals(method)) {
                // Continue
            } else if (RequestLifecycle.isRecognizedHttpMethod(method)) {
                this.methodNotAllowed(this.allowMethod);
                return;
            } else {
                this.notImplemented();
                return;
            }

            if (!this.isAuthorized()) {
                return;
            }

            E req;
            // check if we are doing a get
            if ("GET".equals(method)) {
                try {
                    // make it blank
                    req = this.recordType.getDeclaredConstructor().newInstance();
                } catch (Exception e) {
                    this.sendText(500, e.getMessage());
                    return;
                }
                // otherwise we need to get the req
            } else {
                try {
                    req = JSONMapper.readValue(this.hx.getRequestBody(), this.recordType);
                } catch (JacksonException je) {
                    this.badRequest("Invalid JSON");
                    return;
                }
            }

            if (RequestLifecycle.hasBlankField(req)) {
                this.badRequest("Missing or empty JSON fields.");
                return;
            }

            this.handleDetail(req);
        } finally {
            this.hx.close();
        }
    }

    abstract boolean isAuthorized() throws IOException;
    abstract void handleDetail(E req) throws IOException;

    protected static boolean isRecognizedHttpMethod(String m) {
        return "GET".equals(m) || "HEAD".equals(m) || "POST".equals(m) ||
               "PUT".equals(m) || "DELETE".equals(m) || "OPTIONS".equals(m) ||
               "PATCH".equals(m) || "TRACE".equals(m) || "CONNECT".equals(m);
    }

    protected void sendText(int status, String body) throws IOException {
        byte[] bytes = body.getBytes(StandardCharsets.UTF_8);
        this.hx.getResponseHeaders().set("Content-Type", "text/plain; charset=utf-8");
        this.hx.sendResponseHeaders(status, bytes.length);
        try (OutputStream os = this.hx.getResponseBody()) {
            os.write(bytes);
        }
    }

    protected void badRequest(String msg) throws IOException {
        this.sendText(400, msg);
    }

    protected void conflict(String msg) throws IOException {
        this.sendText(409, msg);
    }

    private void methodNotAllowed(String allow) throws IOException {
        this.hx.getResponseHeaders().set("Allow", allow);
        this.hx.sendResponseHeaders(405, -1);
    }

    private void notImplemented() throws IOException {
        this.hx.sendResponseHeaders(501, -1);
    }


    private static boolean hasBlankField(Record req) {
        for (var component : req.getClass().getRecordComponents()) {
            try {
                String value = (String) component.getAccessor().invoke(req);
                if (isBlank(value)) {
                    return true;
                }
            } catch (IllegalAccessException e) {
                assert(false);
            } catch (InvocationTargetException e) {
                assert(false);
            }
        }
        return false;
    }

    private static boolean isBlank(String s) {
        return s == null || s.trim().isEmpty();
    }
}
