// Copyright (c) 2024 AccelByte Inc. All Rights Reserved.
// This is licensed software from AccelByte Inc, for limitations
// and restrictions contact your company contract manager
namespace AccelByte.ThirdParties.GooglePlayGames
{
    public class AccelByteGooglePlayGames
    {
        internal static System.Func<IGooglePlayGamesImp> ImpGetter;

        internal static IGooglePlayGamesImp Implementation
        {
            get
            {
                IGooglePlayGamesImp retval = null;
                if (ImpGetter == null)
                {
                    if (defaultImp == null)
                    {
                        defaultImp = new NullGooglePlayGamesImp();
                    }
                    retval = defaultImp;
                }
                else
                {
                    retval = ImpGetter.Invoke();
                }
                return retval;
            }
        }

        private static IGooglePlayGamesImp defaultImp;

        /// <summary>
        /// Sign in to googlay play and retrieve google play user id token.
        /// </summary>
        public static Models.AccelByteResult<SignInGooglePlayGamesResult, Core.Error> GetGooglePlayGamesSignInToken()
        {
            return Implementation.GetGooglePlayGamesSignInToken();
        }
    }
}