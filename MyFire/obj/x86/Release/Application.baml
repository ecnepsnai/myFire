   M S B A M L   `   `   `  ���� 
  MyFire    MyFire.Application   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       �� ShutdownMode$  OnMainWindowClose=� �� 	Resources  �� MergedDictionaries �� Source   TweetListTemplete � ColumnDefinitions �� CornerRadius j� Stretch �� ImageSource � ToolTip	 � ContextMenu
 ��Name ��Name J� Opacity �� TextAlignment ��Name  MessageListTemplete �� Padding  UserListTemplete  TweetSearchTemplete  UserCategoryList �Name  TwitterListTemplete  StoredUserTemplete  myFireListBoxStyle �� 	Resources �� EndPoint � MappingMode �� 
StartPoint � Color � Offset��  �� $ Styles_Button.xaml?��� $ Styles_CheckBox.xaml?��� $ Styles_TabItem.xaml?�%�  &	        &	 �    &	 �    &	 �	    &	     &	 �    &	 �    &	     �� .R�� -   $��Stretch=� �� $��60 ��� $��* �.+��� -   $��1q�$ 5��$
��Left=�$��5q�$��50��$��50��$	��Top=����� 
TweetImage��$ UniformToFill=� �� *
TweetImage+ _� .���� .+��� �� �� $
��Bold"�	 �� $
 tweetcm��.@�m� $��Tweet��.@�m� -   $��Start Conversation��m� -   $��Favourite Tweet��m� $��Retweet Tweet��.@�m� -   $��Twitter Style��m� -   $��Quote��m� -   $��Reply To��m� -   $��	Translate��m� -	   $#��Report tweet as advertisement��m� $
��User��.@�m� $��Follow / Unfollow��.@�m� -
   $��Follow��m� -   $��Unfollow��m� -   $��View Profile��m� -   $��Search��m� -   $��Block��m� -   $��Block & Report For Spam���� -   $	��0,5q�$��1�� _� .���� .+��� �� �� $
��Bold"�.+��� $ UsernameLabel��$
��Auto��$	��Top=�$
��Bold"�$
��Auto������ *TweetUsername+�� $ 	TweetText��$
��Auto��$
��Wrap=����� *	TweetText+�� $ TweetAge��$
��Wrap=�$
��Auto��$	 0.5Q�$ Right=�$	��5,0q����� *
TweetAge+�� $
��Auto��$y�	Collapsed=����� *	TweetID+�� $ Report_Button��-   $��1��$��5q�$
��Auto��$��Right=�$��Center=�$
��Spam��$y�	Collapsed=��� .R�� -   $��Stretch=�$��50�� �� $��60 ��� $��* �	 �� .@�m� -   $��Reply to message��m� -   $��Foward message��m� -   $��Translate message���� m� -   $��Report user��m� -   $��Delete message��.+��� -   $ 5��$
��Left=�$��5q�$��50��$��50��$	��Top=����� 
TweetImage��$ UniformToFill=� �� *MessageImage+ _� .���� .+��� �� �� $
��Bold"��� -   $��1�� _� .���� .+��� �� �� $
��Bold"�.+��� $
��Auto��$	��Top=�$
��Bold"�$
��Auto��$��0q����� *MessageUsername+�� $
��Wrap=�$
��Auto������ *MessageText+�� $
��Wrap=�$
��Auto��$	 0.5Q�$ Right=�$ 0,0,2,0q����� *MessageDate+�� .R�� -   $��Stretch=� �� $��60 ��� $��* � _� .���� .+��� �� �� $
��Bold"�.+��� $ 5��$
��Left=�$��5q�$��50��$��50��$	��Top=����� 
TweetImage��$ UniformToFill=� �� *	UserImage+�� $��1��$��5q�.+��� $ UsernameLabel��$
��Bold"����� *UserScreenName+�� $ BioLabel��$
��Auto��$
��Wrap=����� *	UserBio+�� -   $��1��$��5q�$
��Auto��$��Right=�$��Center=�$��Follow��$y�	Collapsed=��� .R�� $��Stretch=�$��50����Z�  �� $��60 ��� $��* �.+��� -   $ 5��$
��Left=�$��5q�$��50��$��50��$	��Top=����� 
TweetImage��$ UniformToFill=� �� *SearchImageSource+ _� .���� $
��Bold"��� $0�*Double click to view more about this user.���� -   $��1�� _� .���� .+��� �� �� $
��Bold"�.+��� $ UsernameLabel��$
��Auto��$	��Top=�$
��Bold"�$
��Auto��$��0q����� *SearchUsername+�� $ 
TweetBlock��$
��Wrap=�$
��Auto������ *
SearchText+�� $ TweetAge��$
��Wrap=�$
��Auto��$	 0.5Q�$ Right=�$ 0,0,2,0q����� *
SearchDate+�� .R�� $ UserCategoryGird��-   .+��� .+��� $
��Bold"����� *CategoryTitle+�� ���� *CategorySize+�� .R�� -   $��Stretch=� _� .���� .+��� �� �� $
��Bold"�$O��IRight click for options, Double Click to view the tweets within the list.��	 �� .@�m� -    $��	Edit List��m� -!   $��Edit List Members���� m� -"   $��Delete List�� �� $��60 ��� $��* �.+��� -#   $ 5��$
��Left=�$��5q�$��50��$��50��$	��Top=����� 
TweetImage��$ UniformToFill=� �� *TwitterListImage+�� $��1��$��5q�.+��� $
��Bold"����� *TwitterListTitle+�� $
��Auto��$
��Wrap=����� *TwitterListDescription+�� .R�� $��Stretch=� �� $��60 ��� $��* �.+��� $ 5��$
��Left=�$��5q�$��50��$��50��$	��Top=����� 
StoreImage��$ UniformToFill=� �� *StoredUserImage+�� $��1��$��5q�.+��� $
��Bold"����� *StoredUserTitle+�� $
��Auto��$
��Wrap=����� *StoredUserDescription+��  ���� $ 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�(��       * SystemColors.HighlightBrushKey+).��� $ 	#FF606060��$ 0Q�� $ 	#FF2F2F2F��$ 1Q��� $ 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�(��       *SystemColors.ControlBrushKey+).��� $ 	#FFEBEBEB��$ 0Q�� $ 	#FF969696��$ 1Q�