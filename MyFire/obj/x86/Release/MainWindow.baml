   M S B A M L   `   `   `  ���� 
  MyFire    MyFire.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$
  Main�� � WindowStartupLocation$ CenterScreen=�
������� � Icon$5 //MyFire;component/Resources/Retro-Fire-Ball.ico��$	��520��$	��830��$	��510�� � TaskbarItemInfo *  $System.Windows.Shell.TaskbarItemInfo     ThumbButtonInfos *  $System.Windows.Shell.ThumbButtonInfo     Description$ Compose new message��   ImageSource$9 3/MyFire;component/Images/home_tabs/tab_messages.png��-   ThumbButtonInfo0��  $ Compose new tweet��$9 3/MyFire;component/Images/home_tabs/tab_timeline.png��-   ThumbButtonInfo1��  $ Show user profile��$7 1/MyFire;component/Images/home_tabs/tab_person.png��-   ThumbButtonInfo2��  $ Quick follow��$9 3/MyFire;component/Images/home_tabs/tab_mentions.png��-   ThumbButtonInfo3��.��� -   $��Wrapper��.+�� -   $��Content������ -   ContentBackground�� j� Stretch$ UniformToFill=�.+�(  "System.Windows.Controls.WebBrowser  -	   $��AnalyticsBrowser��$y�Hidden=�� -
   $��TopBar��$��Stretch=�$	��Top=�$��36��.+��� -    ��Name$ Welcome_Label��$��Loading myFire...��$��Center=�$��20(�
�������	 � ToolTip$	 Oh hai!��
 �� TextAlignment$
 Center=�$��Italic$���Z�  J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ Black��   	Direction$ 0Q�   ShadowDepth$ 0Q�   RenderingBias$ Quality=��� -   $��topmenu_account��$��26��$��73��$
��Left=�$��Center=�$��8,8,0,0q�$��Accounts��   Button_Default#���   �� -   $��topmenu_compose��$��26��$��73��$��Right=�$��Center=�$��0,8,8,0q�$��Compose��#���   �� -   $��topmenu_restart��$��26��$	��113��$
��Left=�$��Center=�$��86,8,0,0q�$��Streaming Error��  
Button_Red#���  $y�	Collapsed=��� $��25��$��Stretch=�$��Bottom=�����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF272727�� � Offset$	 0.5Q�� $ 	#FF141414��$
 0.51Q� J� OpacityMask �� .��� $ Black��$
 0.45Q�� $ Transparent��$
 0.75Q��� -   $��CopyrightInfo��$��Right=�$��Bottom=�
�������$��©��$��5q��� -   $��TabControl1��$��0,36,0,0q� �� TabStripPlacement$ Bottom=���Z� ��Z� .@��� -   $��TimelineTab��  Tab_GlowEffect#���  ����  �� Orientation$ Vertical=�.+��� -   $��tabimg_home��$9��3/MyFire;component/Images/home_tabs/tab_timeline.png��$��17��$��16��	 _� .���� .+��� Access your timeline.�� �� $
��Bold"����	#FF575757 � ContextMenu �� .@�m� -   $��Filter Tweets���� m� -   $
��Bold"�$��Disable Streaming��.��� .+��� -    ��Name$ TMList����.    myFireListBoxStyle#���    TweetListTemplete#���  $��Disabled=�$
��Auto��$��Stretch=�� -   $��Nothing_Tweets��$��50��$	��Top=�$��Stretch=�$y�	Collapsed=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF8D0000��$ 1Q�� $ 	#FFDB0000��$ 0Q�.+��� $��Center=�$
��Left=�$��10q�$��
No tweets!��
�������$��Italic$��� -   $��MentionsTab��#���  ���� $ 
Horizontal=�.+��� -   $��tabimg_connect��$9��3/MyFire;component/Images/home_tabs/tab_mentions.png��$��16��$��16��	 _� .���� .+��� Access your mentions.�� �� $
��Bold"� �� .@�m� -   $��Filter Tweets���� m� -   $
��Bold"�$��Disable Streaming��.��� .+��� -   $ MEList����.  $��Stretch=�#���  ���	#FFBEBEBE
�������#���  $��Disabled=�$
��Auto��� -   $��Nothing_Mentions��$��50��$	��Top=�$��Stretch=�$y�	Collapsed=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF8D0000��$ 1Q�� $ 	#FFDB0000��$ 0Q�.+��� $��Center=�$
��Left=�$��10q�$��No mentions!��
�������$��Italic$��� -   $��MessagesTab��#���  ���� $ 
Horizontal=�.+��� -   $��tabimg_messages��$9��3/MyFire;component/Images/home_tabs/tab_messages.png��$��16��$��16��	 _� .���� .+��� Access your direct messages.�� �� $
��Bold"�.��� .+��� -   $ DMList����.  $��Stretch=�#���  ���	#FFBEBEBE
�������  MessageListTemplete#���  $��Disabled=�$
��Auto��� -    $��Nothing_Messages��$��50��$	��Top=�$��Stretch=�$y�	Collapsed=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF8D0000��$ 1Q�� $ 	#FFDB0000��$ 0Q�.+��� $��Center=�$
��Left=�$��10q�$��No direct messages!��
�������$��Italic$��� -!   $��FavouritesTab��#���  ���� $ 
Horizontal=�.+��� -"   $��tabimg_favourites��$4��./MyFire;component/Images/home_tabs/tab_fav.png��$��15��$��16��	 _� .���� .+��� Access your favourites. �� .@�m� -#   $��Filter Tweets��.��� .+��� -$   $ FAVList����.  $��Stretch=�#���  ���	#FFBEBEBE
�������#���  $��Disabled=�$
��Auto��� $��50��$	��Top=�$��Stretch=�$y�	Collapsed=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF8D0000��$ 1Q�� $ 	#FFDB0000��$ 0Q�.+��� -%   $��Nothing_Favourites��$��Center=�$
��Left=�$��10q�$��No Favourites!��
�������$��Italic$��� -&   $��	SearchTab��#���  ���� $ 
Horizontal=�.+��� -'   $��tabimg_discover��$7��1/MyFire;component/Images/home_tabs/tab_search.png��$��17��$��16��	 _� .���� .+��� 1/Search Twitter, and access the trending topics..��� .+��� $��10q�.+���  �� CornerRadius$ 5��$��1q�$
��Auto��$	��Top=�
������� �� Padding$ 0q�   $ Black��$ 0Q�$ 0Q�._��� $ Vertical=�.+�� -(   $��search_twitter��$
��Auto�� � Cursor$
 Hand��$��0q���Z� .+��� -)   $��SearchTwitter��$��Search Twitter.��$��5q�$��16(�� -*   $��search_input��$y�	Collapsed=�.+��� $ Vertical=�.+��� $��1��$��Stretch=�
��������� -+   $��search_tb_users��$��Users containing null��$��5q�$��16(��� $��1��$��Stretch=�
�������� -,   $��DirectUserGrid��$	��0,5q�.+��� $ Vertical=�.+��� --   $��search_tb_directuser��$��@null��$��5q�$��16(��� $��1��$��Stretch=�
��������� -.   $��search_tb_tweets��$��Tweets containing null��$��5q�$��16(��� $ 5��$��1q�$
��Auto��$	��Top=�
�������$ 0q�$��0,10,0,0q�   $ Black��$ 0Q�$ 0Q�._��� $ Vertical=�.+�� -/   $��search_explore_people��$
��Auto��$
 Hand����Z� .+��� $��13��$-��'/MyFire;component/Images/view_arrow.png��$
��Fill=�$��9��$��Stretch=�$��Right=�$��5q��� $��Explore People��$��5q�$��16(��� $��1��
�������� -0   $��search_explore_toptweets��$
��Auto��$
 Hand����Z� .+��� $��13��$-��'/MyFire;component/Images/view_arrow.png��$
��Fill=�$��9��$��Stretch=�$��Right=�$��5q��� $��Explore Top Tweets��$��5q�$��16(��� $��1��
�������� -1   $��search_explore_trends��$
��Auto��$
 Hand����Z� .+��� $��13��$-��'/MyFire;component/Images/view_arrow.png��$
��Fill=�$��9��$��Stretch=�$��Right=�$��5q��� $��Explore Trending Topics��$��5q�$��16(��� -2   $��
RetweetTab��#���  ���� $ 
Horizontal=�.+��� -3   $��tabimg_retweet��$9��3/MyFire;component/Images/home_tabs/tab_retweets.png��$��17��$��16��	 _� .���� .+��� 75See the tweets that the people you follow retweeted...��� .+��� -4   $ RTList��#���  #���  $��Disabled=�$
��Auto��$��Stretch=�� -5   $��Nothing_Retweets��$��50��$	��Top=�$��Stretch=�$y�	Collapsed=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF8D0000��$ 1Q�� $ 	#FFDB0000��$ 0Q�.+��� $��Center=�$
��Left=�$��10q�$��No retweets!��
�������$��Italic$��� -6   $��BanTab��#���  ���� $ 
Horizontal=�.+��� -7   $��
tabimg_ban��$4��./MyFire;component/Images/home_tabs/tab_ban.png��$��13��$��16��	 _� .���� .+��� )'See a list of tweets that were filterd.�� �� $
��Bold"�'%Be careful about clicking links here!.��� ��Z� .+��� -8   $ FTList��#���  #���  $��Disabled=�$
��Auto��$��Stretch=�� -9   $��Nothing_Filter��$��50��$	��Top=�$��Stretch=�$y�	Collapsed=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF959595��$ 1Q�� $ 	#FFDADADA��$ 0Q�.+��� $��Center=�$
��Left=�$��10q�$)��#No recent tweets have been filterd!��
�������$��Italic$��� -:   $��MeTab��#���  ���� $ 
Horizontal=�.+��� -;   $��	tabimg_me��$7��1/MyFire;component/Images/home_tabs/tab_person.png��$��13��$��16��	 _� .���� .+���  Access the settings of myFire..��� ��Z� .+��� $ Vertical=�$��10q�.+��� -<   $��Border1��$ 5��$��1q�$
��Auto��$	��Top=�
�������$ 0q�   $ Black��$ 0Q�$ 0Q�._��� $ Vertical=�.+�� -=   $��me_about_me��$
��Auto��$
 Hand�����	#01FFFFFF$��0q�.+��� ->   $��me_ProfileImagea��
�������$��1q�$ 5��$
 Hand��$��50��$
��Left=�$��5q�$	��Top=�$��50��$ 5q��� -?   $��me_label_screenname��$��18(�
���   �$
��Auto��$��
58,6,352,0q�$��{ScreenName}��$��CharacterEllipsis=�$
��Wrap=�$	��Top=��� -@   $��
TextBlock1��$��23��$
��Left=�$��	60,31,0,0q�$��View your profile��$	��Top=�
�������$��Italic$��� -A   $��Image4��$��13��$��Right=�$��5q�$
��Fill=�$��Center=�$��9��$-��'/MyFire;component/Images/view_arrow.png���� $��10��$
��Auto���� -B   $��Border2��$ 5��$��1q�$	��Top=�
�������   $ Black��$ 0Q�$ 0Q�._��� .+�� -C   $��me_about_directmessages�����	#01FFFFFF$
 Hand��.+��� -D   $��Image5��$��13��$-��'/MyFire;component/Images/view_arrow.png��$
��Fill=�$��9��$��Stretch=�$��Right=�$��5q��� -E   $��
TextBlock2��$��23��$
��Left=�$��5q�$��Direct Messages��$��Center=�$��16(�$��Normal"�A� -F   $��me_dm_unread_ind��$��10��$��10��$��Right=�$��Center=�$
��20,0q�$y�	Collapsed=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFE3DEFF��$ 0Q�� $ 	#FF796CFF��$ 1Q��� $��1��
�������� -G   $��me_about_lists�����	#01FFFFFF$
 Hand��.+��� $��13��$-��'/MyFire;component/Images/view_arrow.png��$
��Fill=�$��9��$��Stretch=�$��Right=�$��5q��� $��23��$
��Left=�$��5q�$��Lists��$��Center=�$��16(�$��Normal"��� $��10��$
��Auto���� -H   $��Border5��$ 5��$��1q�$	��Top=�
�������   $ Black��$ 0Q�$ 0Q�._��� .+�� -I   $��me_switch_grid�����	#01FFFFFF$
 Hand��.+��� -J   $��Image0��$��13��$-��'/MyFire;component/Images/view_arrow.png��$
��Fill=�$��9��$��Stretch=�$��Right=�$��5q��� -K   $��
TextBlock7��$��23��$
��Left=�$��5q�$��Switch Accounts��$��Center=�$��16(�$��Normal"��� $��1��
�������� -L   $��me_settings_grid�����	#01FFFFFF$
 Hand��.+��� $��13��$-��'/MyFire;component/Images/view_arrow.png��$
��Fill=�$��9��$��Stretch=�$��Right=�$��5q��� $��23��$
��Left=�$��5q�$��Settings��$��Center=�$��16(�$��Normal"��� $��1��
�������� -M   $��me_about_grid�����	#01FFFFFF$
 Hand��.+��� -N   $��Image9��$��13��$-��'/MyFire;component/Images/view_arrow.png��$
��Fill=�$��9��$��Stretch=�$��Right=�$��5q��� -O   $��
TextBlock6��$��23��$
��Left=�$��5q�$��About myFire��$��Center=�$��16(�$��Normal"��� -P   $��	DevButton��$��##��$��5q�$y�	Collapsed=��� -Q   $��BetaTag��$y�	Collapsed=�$��myFire 2.  Beta 1.��$��10q�
�������$
��Left=� �� Padding$ 5q��� -R   $��Fade_Rectangle��
���   ����	#81000000$y�Hidden=�� -S   $��	UsersGrid��$
��Left=�$	��278��$y�Hidden=�
�������.+��� -T   $��ac_user_list��#���    StoredUserTemplete#���  $��6,6,6,34q��� -U   $��Button2��$
��Menu��$��23��$
��Left=�$��6,0,0,5q�$��Bottom=�$��90��#���    �� -V   $��MenuCM��.@�m� -W   $��Feedback��$	 Give feedback on myFire�� m� Icon �� $2��,/MyFire;component/Resources/user_comment.png��m� -X   $��Get Support!��$8	 2Having problems with myFire?  Click here for help!�� �� $'��!/MyFire;component/Images/help.png��m� -Y   $��Quick Follow�� �� $+��%/MyFire;component/Images/user_add.png��m� -Z   $��
Undo Tweet��$	 Undo your last tweet!�� �� $-��'/MyFire;component/Images/arrow_undo.png���� m� -[   $��Account Managment�� �� $'��!/MyFire;component/Images/user.png��m� -\   $��Settings��$&	  Tweat myFire to suit your needs!�� �� $,��&/MyFire;component/Resources/wrench.png��