mergeInto(LibraryManager.library, {

    YandexSDKReady: function () {
        ysdk.features.LoadingAPI.ready();
    },

    YandexSDKStart: function () {
        ysdk.features.GameplayAPI.start();
    },

    YandexSDKStop: function () {
        ysdk.features.GameplayAPI.stop();
    },
});