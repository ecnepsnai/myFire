   M S B A M L   `   `   `  ���� 
  MyFire    MyFire.Settings   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  myFire | Settings��$	��350��$	��397�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Icon$5 //MyFire;component/Resources/Retro-Fire-Ball.ico��.��� ���	#FF484848.+��� -    ��Name$ TabControl1��$��12q�.@��� -    ��Name$ TabItem1��$��General��.��� .+��� $��5,5,5,0q�$	��Top=�.+��� -   $��	btn_Reset��$��Reset All Settings��$��0,0,0,5q�   
Button_Red#���   $��23�� � ToolTip _� .��� .+��� .+��� $<��6Resets all of myFire's settings to their default sate.���� ��  �� Orientation$ 
Horizontal=�.+��� $��16��$��16��$P��J/MyFire;component/Images/Annotations/109_AllAnnotations_Error_16x16_72.png��$��0,0,5,0q��� $|��vCareful!  When you reset all of the settings myFire logs out of all your accounts and will require you to log back in.��$
��Bold"�$
��Wrap=��� -   $��btn_DataCollection��$��Error reporting.��$��0,0,0,5q�  Button_Default#���  $��23���� -   $��
TextBlock1��$��23��$
��Left=�$��6,63,0,0q�$��
Font Size:��$	��Top=�$��60�� �� TextAlignment$ Right=�$y�Hidden=��� $	��Top=�$��Stretch=�$��6,66,4,0q�.+��� -   $��
cb_closemy��$��Close myFire to the tray.��$	��Top=�$E ?Right click the myFire icon in the system tray to close myFire.��$��0,5,5,5q��� -   $��cb_backgrounding��$��Tweet in background.��$	��Top=�$��0,5,5,5q� � .+��� .+��� $^��XIf you disable this option, The New Tweet dialog will not close until the tweet is sent.���� �� $ 
Horizontal=�.+��� $��16��$��16��$R��L/MyFire;component/Images/Annotations/109_AllAnnotations_Warning_16x16_72.png��$��0,0,5,0q��� $_��YWe recommend you leave this enabled, unless your computer has serious performance issues.��$
��Bold"�$
��Wrap=��� $��Security��.��� .+��� $��5q�.+��� -   $��btn_filters��$��Manage Filters��#���  $��23��$��5,5,5,0q��� -	   $��cb_ads��$��5q����� $ 
Horizontal=�.+��� $)��#Automatically block advertisements.���� -
   $��	about_ads��$	�� ? ��
����  �$
��Bold"�	 � Cursor$
	 Hand���� -   $��cb_tco��$��5q�$��Skip t.co links�� � .+��� .+��� $A��;Skip the t.co redirection and navigate directly to the url.���� �� $ 
Horizontal=�.+��� $��16��$��16��$R��L/MyFire;component/Images/Annotations/109_AllAnnotations_Warning_16x16_72.png��$��0,0,5,0q��� $[��UWe recommend you leave this enabled, unless your network does not allow redirections.��$
��Bold"�$
��Wrap=��� $��Display��.��� .+��� .��� .+��� $��2q�.+��� $��Home Screen Tabs��.��� .+��� .+��� -   $��cb_tab_timeline��$��Show timeline tab��$��16��$
��Left=�$	��Top=�$	��211��$��5q��� -   $��cb_tab_mentions��$��Show mentions tab��$��16��$
��Left=�$	��Top=�$��5,0,5,5q��� -   $��cb_tab_messages��$��Show messages tab��$��16��$
��Left=�$	��Top=�$��5,0,5,5q��� -   $��cb_tab_favourites��$��Show favourites tab��$��16��$
��Left=�$	��Top=�$��5,0,5,5q��� -   $��cb_tab_search��$��Show search tab��$��16��$
��Left=�$	��Top=�$��5,0,5,5q��� -   $��
cb_tab_ban��$��Show filters tab��$��16��$
��Left=�$	��Top=�$��5,0,5,5q��� $��	Font Size��.��� .+��� .+�	� -   $��rb_font_tiny��
 	� 	GroupName$
 FontSize��$
��Tiny��$��5q�	� -   $��rb_font_small��$
 FontSize��$��Small��$��5,0,5,5q�	� -   $��rb_font_medium��$
 FontSize��$��Medium��$��5,0,5,5q�	� -   $��rb_font_large��$
 FontSize��$��Large��$��5,0,5,5q�	� -   $��rb_font_huge��$
 FontSize��$
��Huge��$��5,0,5,5q��� $��Display Name��.��� .+��� .+�	� -   $��rb_name_name��$
 DisplayName��$��	Full Name��$��5q�	� -   $��rb_name_username��$
 DisplayName��$��Screen Name��$��5,0,5,5q��� $��Date Format��.��� .+��� .+�	� -   $��rb_date_relitave��$
 DisplayDate��$��Relative��$��5q�	� -   $��rb_date_absolute��$
 DisplayDate��$��Absolute��$��5,0,5,5q��� $��Main Window Design��.��� .+��� .+�	� -   $��rb_design_twitter��$
 BackgroundImage��$#��Your Twitter background image��$��5q�	� -   $��rb_design_bing��$
 BackgroundImage��$��Bing image of the day��$��5,0,5,5q��� $��API Control��.��� .+��� -   $��	GroupBox1��$��API Call managment��$��6,6,6,0q�$	��Top=�.��� .+��� .+��� -   $#��cb_api_NativeFollowersFriends��$*��$Load followers and friends in myFire��$��16��$
��Left=�$	��Top=�$	��211��$e _Disable this option to save API calls but Followers and Friends list will load in your browser.��$��5q��� -   $��cb_api_ProfileLoad��$!��Load user profile in myFire��$��16��$
��Left=�$	��Top=�$a [Disable this option to save API calls, but all profiles will be loaded in your web browser.��$��5,0,5,5q��� -    $��cb_api_TimelineLaunch��$��Load timeline at launch��$��16��$
��Left=�$	��Top=�$` ZDisable this option to skip the timeline load at lauch, streaming will still run, however.��$��5,0,5,5q��� -!   $��cb_api_InteractiveMaps��$"��Use interactive maps control��$��16��$
��Left=�$	��Top=�$s mDisable this option to not use the interactive maps control, maps will be loaded using your internet browser.��$��5,0,5,5q��� -"   $��cb_api_PopulateAutobox��$.��(Populate autocomplete box with followers��$��16��$
��Left=�$	��Top=�$j dDisable this option to not populate the Autocomplete box in the User Search box with your followers.��$��5,0,5,5q��� $ 
Horizontal=�.+��� $�� Timeline has: ��$��Center=��� -#   $��TweetNumber��$	��100���� $��	 tweets. ��$��Center=��� $��(Default: 100)��$��Center=�
�������$��10(��� -$   $ TabItem2��$��Notifications��.��� .+��� -%   $��btn_TestNotification��$��Test Notification��$��23��$��6,0,6,6q�$��Bottom=�  Button_Green#���  �� -&   $��
TextBlock2��$��Right=�$��0,5,20,0q�$��Notification       Sound��$	��Top=��� -'   $"��cb_notification_window_tweet��$��Right=�$��
0,27,103,0q�$��16��$	��Top=��� -(   $!��cb_notification_sound_tweet��$��16��$��Right=�$��	0,27,32,0q�$	��Top=��� -)   $%��cb_notification_window_mentions��$��16��$��Right=�$��
0,48,103,0q�$	��Top=��� -*   $#��cb_notification_sound_mention��$��Right=�$��	0,48,32,0q�$��16��$	��Top=��� -+   $$��cb_notification_window_message��$��16��$��Right=�$��
0,68,103,0q�$	��Top=��� -,   $#��cb_notification_sound_message��$��16��$��Right=�$��	0,68,32,0q�$	��Top=��� $��52.04��$��11,28,143,0q�$
��Wrap=�$	��Top=�$ Right=�$	��187����  � Language$ en-ca�$�When somebody tweets:���� �� $ en-ca�$!�When somebody mentions you:���� �� $ en-ca�$!�When somebody messages you:��� --   $��Grid1��$	��100��$��0,0,0,41q�$��Bottom=�$��Center=�$	��200��
����iA�.+�� $��Bottom=�$��15��
����  �.+��� $
��Left=�$��25��
��� � �	� -.   $#��rb_notification_position_left��$
 Position��$	��Top=�$
��Left=�$��5q�	� -/   $$��rb_notification_position_right��$
 Position��$	��Top=�$��Right=�$��5q��� -0   $��
TextBlock3��$
��Left=�$��	70,92,0,0q�$#��Notification window position:��$	��Top=�