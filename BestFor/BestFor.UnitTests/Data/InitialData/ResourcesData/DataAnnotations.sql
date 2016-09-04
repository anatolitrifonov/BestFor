-- This file contains strings for data annotations.

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationErrorMessageRequiredEmail')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationErrorMessageRequiredEmail',
		N'Email is required.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationErrorMessageRequiredEmail')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationErrorMessageRequiredEmail',
		N'Популярные сегодня', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationValidationMessageEmailAddress')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationValidationMessageEmailAddress',
		N'Email address format.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationValidationMessageEmailAddress')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationValidationMessageEmailAddress',
		N'Популярные сегодня', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationDisplayNameEmail')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationDisplayNameEmail',
		N'Email', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationDisplayNameEmail')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationDisplayNameEmail',
		N'Популярные сегодня', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationErrorMessageRequiredPassword')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationErrorMessageRequiredPassword',
		N'Password is required.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationErrorMessageRequiredPassword')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationErrorMessageRequiredPassword',
		N'Популярные сегодня', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationErrorMessageStringLength100X6Password')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationErrorMessageStringLength100X6Password',
		N'Password must be 6 to 100 characters.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationErrorMessageStringLength100X6Password')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationErrorMessageStringLength100X6Password',
		N'Популярные сегодня', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationDisplayNamePassword')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationDisplayNamePassword',
		N'Password', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationDisplayNamePassword')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationDisplayNamePassword',
		N'Популярные сегодня', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationDisplayNameConfirmPassword')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationDisplayNameConfirmPassword',
		N'Confirm Password', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationDisplayNameConfirmPassword')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationDisplayNameConfirmPassword',
		N'Популярные сегодня', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationErrorMessageComparePassword')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationErrorMessageComparePassword',
		N'Password and Confirm Password do not match.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationErrorMessageComparePassword')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationErrorMessageComparePassword',
		N'Популярные сегодня', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationErrorMessageRequiredUserName')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationErrorMessageRequiredUserName',
		N'User Name is required.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationErrorMessageRequiredUserName')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationErrorMessageRequiredUserName',
		N'Укажите Имя Пользователя', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationErrorMessageStringLength100X6UserName')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationErrorMessageStringLength100X6UserName',
		N'User Name must be 6 to 100 characters.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationErrorMessageStringLength100X6UserName')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationErrorMessageStringLength100X6UserName',
		N'Имя Пользователя должен быть между 6 и 100 символов.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationErrorMessageStringLength100X4UserName')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationErrorMessageStringLength100X4UserName',
		N'User Name must be 4 to 100 characters.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationErrorMessageStringLength100X4UserName')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationErrorMessageStringLength100X4UserName',
		N'Имя Пользователя должен быть между 4 и 100 символов.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationDisplayNameUserName')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationDisplayNameUserName',
		N'User Name', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationDisplayNameUserName')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationDisplayNameUserName',
		N'Имя Пользователя', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationErrorMessageStringLength100X6DisplayName')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationErrorMessageStringLength100X6DisplayName',
		N'Display Name must be 6 to 100 characters.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationErrorMessageStringLength100X6DisplayName')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationErrorMessageStringLength100X6DisplayName',
		N'Псевдоним должен быть между 6 и 100 символов.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationErrorMessageStringLength100X3DisplayName')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationErrorMessageStringLength100X3DisplayName',
		N'Display Name must be 3 to 100 characters.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationErrorMessageStringLength100X3DisplayName')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationErrorMessageStringLength100X3DisplayName',
		N'Псевдоним должен быть между 3 и 100 символов.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationDisplayNameDisplayName')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationDisplayNameDisplayName',
		N'Display Name', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationDisplayNameDisplayName')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationDisplayNameDisplayName',
		N'Псевдоним', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationErrorMessageRequiredReason')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationErrorMessageRequiredReason',
		N'Please specify the reason.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationErrorMessageRequiredReason')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationErrorMessageRequiredReason',
		N'Пожалуйста укажите причину.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationDisplayNameReason')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationDisplayNameReason',
		N'Reason', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationDisplayNameReason')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationDisplayNameReason',
		N'Причина', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'AnnotationErrorMessageStringLength1000X3Reason')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'AnnotationErrorMessageStringLength1000X3Reason',
		N'Reason must be 6 to 100 characters.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'AnnotationErrorMessageStringLength1000X3Reason')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'AnnotationErrorMessageStringLength1000X3Reason',
		N'Причина должна быть от 6 до 1000 символов.', getDate());
GO