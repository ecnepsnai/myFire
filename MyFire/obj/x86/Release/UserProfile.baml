   M S B A M L   `   `   `  ���� 
  MyFire    MyFire.UserProfile   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Adxm1http://schemas.devexpress.com/winfx/2008/xaml/map       � Title$  MyFire | {screen_name}��$	��546��$	��592�� � WindowStartupLocation$ CenterScreen=� J� Opacity$ 1Q� � Icon$5 //MyFire;component/Resources/Retro-Fire-Ball.ico����Z� .��� -   $��Grid1������ -   BackgroundImage�� j� Stretch$ UniformToFill=�.+���  �� Orientation$ 
Horizontal=�$	��Top=�.+��� -   $��1q� �� CornerRadius$ 5��$
��Left=�$��5q�$��75��$��75��$	��Top=����� -   TweetUserImage��$ UniformToFill=��� $ Vertical=�$��0,5,0,0q�.+��� -   $��	user_name��$
��Wrap=�$��null:screenname��$��24(�
������� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ Black��	   	Direction$	 0Q�
   ShadowDepth$
 0Q��� -   $��user_screenname��$
��Wrap=�$��	null:name��$	��Top=�$��14(�
�������   $ Black��$	 0Q�$
 0Q��� -   $��user_identity��$
��Wrap=�$��null:identity��$	��Top=�$��14(�
�������   $ Black��$	 0Q�$
 0Q��� $ 2�����	#FF919191$��1q�$	��Top=�$��Right=�$��	0,33,10,0q�����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ White�� � Offset$ 0Q�� $ 	#FFE6E6E6��$ 1Q�._�� .+��� $ 
Horizontal=�.+��� -   $��FollowButton��$��Follow��$��5q� � Cursor$
 Hand���� $��.5��$��Stretch=�
��������� -	   $��Image1��$'��!/MyFire;component/Images/user.png��$��15��$��5q�$
 Hand�� � ContextMenu �� -
   $��User_CM��.@�m� -   $��Mention��m� -   $��Direct Message��m� -   $��Toggle Mute���� m� -   $��Block��m� -   $��Block and Report���� $��25��$��Stretch=�$��Bottom=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF272727��$	 0.5Q�� $ 	#FF141414��$
 0.51Q� J� OpacityMask �� .��� $ Black��$
 0.45Q�� $ Transparent��$
 0.75Q��� -   $��TabControl1��$��Stretch=� �� TabStripPlacement$ Bottom=�$��0,91,0,0q���Z� ��Z� .@��� -   $��UserTab��   Tab_GlowEffect#���   ���� $ Vertical=�.+��� $7��1/MyFire;component/Images/home_tabs/tab_person.png��$��13��$��16��.��� .+��� .+��� $ 5��$	��Top=�$��Stretch=�$
��10,5q�
�������   $ Black��$	 0Q�$
 0Q�._��� .+��� -   $��me_about_description��.+��� -   $��User_Description��$��Null-Description��$��5q� �� TextAlignment$ Center=�$
��Wrap=��� -   $��me_about_url��.+��� $��1��$��Stretch=�
��������� -   $��User_URL��$��Null-URL��$��5q�$ Center=�$
��Wrap=��� -   $��me_about_location��.+��� $��1��$��Stretch=�
��������� -   $��User_location��$��Null-Location��$��5q�$ Center=�$
��Wrap=��� $ 5��$	��Top=�$��Stretch=�$
��10,5q�
�������   $ Black��$	 0Q�$
 0Q�._��� .+�� -   $��me_stats_tweets�����	#01FFFFFF$
 Hand��.+��� $��13��$-��'/MyFire;component/Images/view_arrow.png��$
��Fill=�$��9��$��Stretch=�$��Right=�$��5q��� $��23��$
��Left=�$��5q�$��Tweets��$��Center=�$��16(�$��Normal"��� $ 5��$��Right=�$��Center=�$��0,0,20,0q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF004786��$ 0Q�� $ 	#FF008CFF��$ 1Q�._��� -   $��User_Tweets��
�������$
��NULL��$��2q�$��9(��� $��1��
�������� -   $��me_stats_followers�����	#01FFFFFF$
 Hand��.+��� $��13��$-��'/MyFire;component/Images/view_arrow.png��$
��Fill=�$��9��$��Stretch=�$��Right=�$��5q��� $��23��$
��Left=�$��5q�$��	Followers��$��Center=�$��16(�$��Normal"��� $ 5��$��Right=�$��Center=�$��0,0,20,0q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF004786��$ 0Q�� $ 	#FF008CFF��$ 1Q�._��� -   $��User_Followers��
�������$
��NULL��$��2q�$��9(��� $��1��
�������� -   $��me_stats_friends�����	#01FFFFFF$
 Hand��.+��� $��13��$-��'/MyFire;component/Images/view_arrow.png��$
��Fill=�$��9��$��Stretch=�$��Right=�$��5q��� $��23��$
��Left=�$��5q�$��	Following��$��Center=�$��16(�$��Normal"��� $ 5��$��Right=�$��Center=�$��0,0,20,0q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF004786��$ 0Q�� $ 	#FF008CFF��$ 1Q�._��� -   $��User_Friends��
�������$
��NULL��$��2q�$��9(��� $��1��
�������� -   $��me_stats_favourites�����	#01FFFFFF$
 Hand��.+��� $��13��$-��'/MyFire;component/Images/view_arrow.png��$
��Fill=�$��9��$��Stretch=�$��Right=�$��5q��� $��23��$
��Left=�$��5q�$��
Favourites��$��Center=�$��16(�$��Normal"��� $ 5��$��Right=�$��Center=�$��0,0,20,0q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF004786��$ 0Q�� $ 	#FF008CFF��$ 1Q�._��� -   $��User_Favourites��
�������$
��NULL��$��2q�$��9(��� -    $��about_grid_more��$ 5��$	��Top=�$��Stretch=�$
��10,5q�
�������   $ Black��$	 0Q�$
 0Q�._�9� $��More Information��$��5q�.��� .+��� .+��� -!   $��User_More_ID���� -"   $��User_More_CreateDate���� -#   $��User_More_LastTweet���� -$   $��User_More_Languange���� -%   $��TimelineTab��#���   ���� $ Vertical=�.+��� $9��3/MyFire;component/Images/home_tabs/tab_timeline.png��$��17��$��16��.��� .+��� -&    ��Name$ TMList����.    myFireListBoxStyle#���  ���	#FFBEBEBE
�������  TweetListTemplete#���  $��Disabled=�$
��Auto��$��Stretch=��� -'   $��MentionsTab��#���   ���� $ Vertical=�.+��� $9��3/MyFire;component/Images/home_tabs/tab_mentions.png��$��16��$��16��.��� .+��� -(   $ MEList����.  #���  ���	#FFBEBEBE
�������#���  $��Disabled=�$
��Auto��$��Stretch=��� -)   $��FavouritesTab��#���   ���� $ Vertical=�.+��� $4��./MyFire;component/Images/home_tabs/tab_fav.png��$��15��$��16��.��� .+��� -*   $ FAVList����.  #���  ���	#FFBEBEBE
�������#���  $��Disabled=�$
��Auto��$��Stretch=�