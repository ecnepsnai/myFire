   M S B A M L   `   `   `  ����
  MyFire    MyFire.Settings   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6B     � Title$  myFire | Settings��5      $	��350��6   $	��397��6,    � 
ResizeMode$ NoResize=�68    � WindowStartupLocation$ CenterScreen=�6N    � Icon$5 //MyFire;component/Resources/Retro-Fire-Ball.ico��6s   .��5      � ���	#FF484848.+�5   
   �� -   5         ��Name$ TabControl1��5   !   $��12q�6   .@�5      �� -   5         ��Name$ TabItem1��5   '   $��General��6   .��5	      � .+�5
      �� $��5,5,5,0q�6!   $	��Top=�62   .+�5      �� -   5        $��	btn_Reset��5   >   $��Reset All Settings��6!   $��0,0,0,5q�6O      
Button_Red#���   6`   $��23��6�    � ToolTip 5      _� 5   "   .��5   &   � .+�5   *   �� .+�5   .   �� $<��6Resets all of myFire's settings to their default sate.��68   �� 5   .   �� 5   .    �� Orientation$ 
Horizontal=�69   .+�5   2   �� $��16��68   $��16��6D   $P��J/MyFire;component/Images/Annotations/109_AllAnnotations_Error_16x16_72.png��6O   $��0,0,5,0q�6�   �� 5   2   $|��vCareful!  When you reset all of the settings myFire logs out of all your accounts and will require you to log back in.��6<   $
��Bold"�6�   $
��Wrap=�6�   5   /   5   +   5   '   5   #   5      �� 5      -   5        $��btn_DataCollection��5   <   $��Error reporting.��6!   $��0,0,0,5q�6V     Button_Default#���  6g   $��23��6�   5      �� 5      -   5        $��
TextBlock1��5   Y   $��23��6    $
��Left=�6,   $��6,63,0,0q�6G   $��
Font Size:��6k   $	��Top=�6}   $��60��6�    �� TextAlignment$ Right=�6�   $y�Hidden=�6�   �� 5      $	��Top=�6!   $��Stretch=�69   $��6,66,4,0q�6W   .+�5      �� -   5        $��
cb_closemy��5   G   $��Close myFire to the tray.��6#   $	��Top=�6Y   $E ?Right click the myFire icon in the system tray to close myFire.��6q   $��0,5,5,5q�6�   �� 5       -   5        $��cb_backgrounding��5    C   $��Tweet in background.��6#   $	��Top=�6[   $��0,5,5,5q�6s    5!      � 5"   "   .+�5#   &   �� .+�5$   *   �� $^��XIf you disable this option, The New Tweet dialog will not close until the tweet is sent.��64   �� 5%   *   �� 5&   *   $ 
Horizontal=�65   .+�5'   .   �� $��16��64   $��16��6@   $R��L/MyFire;component/Images/Annotations/109_AllAnnotations_Warning_16x16_72.png��6K   $��0,0,5,0q�6�   �� 5(   .   $_��YWe recommend you leave this enabled, unless your computer has serious performance issues.��68   $
��Bold"�6�   $
��Wrap=�6�   5)   +   5*   '   5+   #   5-      5.      5/      50      �� 51      $��Security��6   .��52      � .+�53      �� $��5q�6!   .+�54      �� -   5        $��btn_filters��54   !   $��Manage Filters��64   #���  6M   $��23��6v   $��5,5,5,0q�6�   �� 55      -	   5        $��cb_ads��55   #   $��5q�61   ��56      �� 57   "   $ 
Horizontal=�6-   .+�58   &   �� $)��#Automatically block advertisements.��60   �� 59   &   -
   5        $��	about_ads��59   _   $	�� ? ��60   
����  �$
��Bold"�6M   	 � Cursor$
	 Hand��6p   5:   #   5<      �� 5=      -   5        $��cb_tco��5=   #   $��5q�61   $��Skip t.co links��6<    5>      � 5?   "   .+�5@   &   �� .+�5A   *   �� $A��;Skip the t.co redirection and navigate directly to the url.��64   �� 5B   *   �� 5C   *   $ 
Horizontal=�65   .+�5D   .   �� $��16��64   $��16��6@   $R��L/MyFire;component/Images/Annotations/109_AllAnnotations_Warning_16x16_72.png��6K   $��0,0,5,0q�6�   �� 5E   .   $[��UWe recommend you leave this enabled, unless your network does not allow redirections.��68   $
��Bold"�6�   $
��Wrap=�6�   5F   +   5G   '   5H   #   5J      5K      5L      5M      �� 5N      $��Display��6   .��5O      � .+�5P      �� .��5Q      � .+�5R      �� $��2q�6)   .+�5S   "   �� $��Home Screen Tabs��6+   .��5T   &   � .+�5U   *   �� .+�5V   .   �� -   5        $��cb_tab_timeline��5V   z   $��Show timeline tab��67   $��16��6S   $
��Left=�6_   $	��Top=�6�   $	��211��6�   $��5q�6�   �� 5W   .   -   5        $��cb_tab_mentions��5W   z   $��Show mentions tab��67   $��16��6S   $
��Left=�6_   $	��Top=�6�   $��5,0,5,5q�6�   �� 5X   .   -   5        $��cb_tab_messages��5X   z   $��Show messages tab��67   $��16��6S   $
��Left=�6_   $	��Top=�6�   $��5,0,5,5q�6�   �� 5Y   .   -   5        $��cb_tab_favourites��5Y   |   $��Show favourites tab��67   $��16��6U   $
��Left=�6a   $	��Top=�6�   $��5,0,5,5q�6�   �� 5Z   .   -   5        $��cb_tab_search��5Z   x   $��Show search tab��67   $��16��6Q   $
��Left=�6]   $	��Top=�6�   $��5,0,5,5q�6�   �� 5[   .   -   5        $��
cb_tab_ban��5[   y   $��Show filters tab��67   $��16��6R   $
��Left=�6^   $	��Top=�6�   $��5,0,5,5q�6�   5\   +   5]   '   5^   #   �� 5_   "   $��	Font Size��6+   .��5`   &   � .+�5a   *   �� .+�5b   .   	� -   5        $��rb_font_tiny��5b   ^   
 	� 	GroupName$
 FontSize��6:   $
��Tiny��6O   $��5q�6r   	� 5c   .   -   5        $��rb_font_small��5c   _   $
 FontSize��6:   $��Small��6O   $��5,0,5,5q�6t   	� 5d   .   -   5        $��rb_font_medium��5d   `   $
 FontSize��6:   $��Medium��6O   $��5,0,5,5q�6v   	� 5e   .   -   5        $��rb_font_large��5e   _   $
 FontSize��6:   $��Large��6O   $��5,0,5,5q�6t   	� 5f   .   -   5        $��rb_font_huge��5f   ^   $
 FontSize��6:   $
��Huge��6O   $��5,0,5,5q�6r   5g   +   5h   '   5i   #   �� 5j   "   $��Display Name��6+   .��5k   &   � .+�5l   *   �� .+�5m   .   	� -   5        $��rb_name_name��5m   f   $
 DisplayName��6:   $��	Full Name��6R   $��5q�6z   	� 5n   .   -   5        $��rb_name_username��5n   h   $
 DisplayName��6:   $��Screen Name��6R   $��5,0,5,5q�6�   5o   +   5p   '   5q   #   �� 5r   "   $��Date Format��6+   .��5s   &   � .+�5t   *   �� .+�5u   .   	� -   5        $��rb_date_relitave��5u   e   $
 DisplayDate��6:   $��Relative��6R   $��5q�6}   	� 5v   .   -   5        $��rb_date_absolute��5v   e   $
 DisplayDate��6:   $��Absolute��6R   $��5,0,5,5q�6}   5w   +   5x   '   5y   #   �� 5z   "   $��Main Window Design��6+   .��5{   &   � .+�5|   *   �� .+�5}   .   	� -   5        $��rb_design_twitter��5}   ~   $
 BackgroundImage��6:   $#��Your Twitter background image��6V   $��5q�6�   	� 5~   .   -   5        $��rb_design_bing��5~   v   $
 BackgroundImage��6:   $��Bing image of the day��6V   $��5,0,5,5q�6�   5   +   5�   '   5�   #   5�      5�      5�      5�      5�      �� 5�      $��API Control��6   .��5�      � .+�5�      �� -   5        $��	GroupBox1��5�   L   $��API Call managment��6   $��6,6,6,0q�6;   $	��Top=�6]   .��5�      � .+�5�      �� .+�5�   "   �� -   5        $#��cb_api_NativeFollowersFriends��5�   �   $*��$Load followers and friends in myFire��6+   $��16��6Z   $
��Left=�6f   $	��Top=�6�   $	��211��6�   $e _Disable this option to save API calls but Followers and Friends list will load in your browser.��6�   $��5q�64  �� 5�   "   -   5        $��cb_api_ProfileLoad��5�   x   $!��Load user profile in myFire��6+   $��16��6Q   $
��Left=�6]   $	��Top=�6�   $a [Disable this option to save API calls, but all profiles will be loaded in your web browser.��6�   $��5,0,5,5q�6  �� 5�   "   -    5        $��cb_api_TimelineLaunch��5�   t   $��Load timeline at launch��6+   $��16��6M   $
��Left=�6Y   $	��Top=�6�   $` ZDisable this option to skip the timeline load at lauch, streaming will still run, however.��6�   $��5,0,5,5q�6  �� 5�   "   -!   5        $��cb_api_InteractiveMaps��5�   y   $"��Use interactive maps control��6+   $��16��6R   $
��Left=�6^   $	��Top=�6�   $s mDisable this option to not use the interactive maps control, maps will be loaded using your internet browser.��6�   $��5,0,5,5q�6'  �� 5�   "   -"   5        $��cb_api_PopulateAutobox��5�   �   $.��(Populate autocomplete box with followers��6+   $��16��6^   $
��Left=�6j   $	��Top=�6�   $j dDisable this option to not populate the Autocomplete box in the User Search box with your followers.��6�   $��5,0,5,5q�6*  �� 5�   "   $ 
Horizontal=�6-   .+�5�   &   �� $�� Timeline has: ��60   $��Center=�6G   �� 5�   &   -#   5        $��TweetNumber��5�   .   $	��100��6A   �� 5�   &   $��	 tweets. ��60   $��Center=�6A   �� 5�   &   $��(Default: 100)��60   $��Center=�6F   
�������$��10(�6s   5�   #   5�      5�      5�      5�      5�      �� 5�      -$   5        $ TabItem2��5�   -   $��Notifications��6   .��5�      � .+�5�      �� -%   5        $��btn_TestNotification��5�   V   $��Test Notification��6   $��23��69   $��6,0,6,6q�6E   $��Bottom=�6r     Button_Green#���  6�   �� 5�      -&   5        $��
TextBlock2��5�   N   $��Right=�6    $��0,5,20,0q�6<   $��Notification       Sound��6`   $	��Top=�6�   �� 5�      -'   5        $"��cb_notification_window_tweet��5�   O   $��Right=�6   $��
0,27,103,0q�6;   $��16��6s   $	��Top=�6   �� 5�      -(   5        $!��cb_notification_sound_tweet��5�   Z   $��16��6   $��Right=�6+   $��	0,27,32,0q�6G   $	��Top=�6}   �� 5�      -)   5        $%��cb_notification_window_mentions��5�   [   $��16��6   $��Right=�6+   $��
0,48,103,0q�6G   $	��Top=�6�   �� 5�      -*   5        $#��cb_notification_sound_mention��5�   N   $��Right=�6   $��	0,48,32,0q�6;   $��16��6s   $	��Top=�6   �� 5�      -+   5        $$��cb_notification_window_message��5�   [   $��16��6   $��Right=�6+   $��
0,68,103,0q�6G   $	��Top=�6�   �� 5�      -,   5        $#��cb_notification_sound_message��5�   Z   $��16��6   $��Right=�6+   $��	0,68,32,0q�6G   $	��Top=�6   �� 5�      $��52.04��6    $��11,28,143,0q�6/   $
��Wrap=�6D   $	��Top=�6X   $ Right=�6p   $	��187��6�   �� 5�       � Language$ en-ca�6   $�When somebody tweets:��6/   �� 6N   �� 5�      $ en-ca�6   $!�When somebody mentions you:��6/   �� 6T   �� 5�      $ en-ca�6   $!�When somebody messages you:��6/   5�      � 5�      --   5        $��Grid1��5�   :   $	��100��6   $��0,0,0,41q�6(   $��Bottom=�6G   $��Center=�6b   $	��200��6   
����iA�.+�5�      � $��Bottom=�6   $��15��6:   
����  �.+�5�      �� $
��Left=�6(   $��25��6C   
��� � �6N   5�      	� 5�      -.   5        $#��rb_notification_position_left��5�   y   $
 Position��6&   $	��Top=�6;   $
��Left=�6S   $��5q�6n   6y   	� 5�      -/   5        $$��rb_notification_position_right��5�   z   $
 Position��6&   $	��Top=�6;   $��Right=�6S   $��5q�6o   6z   5�      �� 5�      -0   5        $��
TextBlock3��5�   N   $
��Left=�6    $��	70,92,0,0q�6;   $#��Notification window position:��6`   $	��Top=�6�   5�      5�      5�      5�      5�      