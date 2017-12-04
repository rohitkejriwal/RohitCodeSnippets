package sg.lpa;

import android.app.Application;


public class LPAApplication extends Application {

    private static String authToken;

    public static String getAuthToken() {
        return authToken;
    }

    public static void setAuthToken(String userAuthToken) {
        authToken = userAuthToken;
    }
}
