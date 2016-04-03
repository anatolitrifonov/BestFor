--use bestfor
--go

--select * from ResourceString
--GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'trending_today')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'trending_today', N'Treding Today', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'trending_today')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'trending_today', N'Популярные сегодня', getDate());

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'trending_overall')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'trending_overall', N'Treding Overall', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'trending_overall')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'trending_overall', N'Популярные за все время', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'suggestion_panel_initial_message')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_initial_message', N'Enter the first word', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_initial_message')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_initial_message', N'Добавьте первое слово', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'suggestion_panel_no_answers_found')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_no_answers_found', N'No answers found. Be the first!', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_no_answers_found')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_no_answers_found', N'Ответов не найдено. Будьте первым!', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'suggestion_panel_x_answers_found')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_x_answers_found',
		N'answers found. Do you have your own? Or vote for the answer below?', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_x_answers_found')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_x_answers_found',
		N'ответов. Добавьте свой или проголосуйте за уже существующий?', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'suggestion_panel_error_happened_searching_answers')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_error_happened_searching_answers',
		N'Error happened while searching for answers', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_error_happened_searching_answers')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_error_happened_searching_answers',
		N'Ошибка при поиске ответов', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'suggestion_panel_you_were_the_first')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_you_were_the_first',
		N'You were the first to say that', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_you_were_the_first')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_you_were_the_first',
		N'Вы были первым, сказавшим ', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'best_start_capital')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'best_start_capital', N'Best', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'best_start_capital')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'best_start_capital', N'Лучший', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'for_lower')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'for_lower', N'for', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'for_lower')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'for_lower', N'для', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'is_lower')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'is_lower', N'is', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'is_lower')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'is_lower', N'это', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'suggestion_panel_your_answer')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_your_answer', N'Your answer', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_your_answer')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_your_answer', N'Ваш ответ', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'suggestion_panel_was_added')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_was_added', N'was added', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_was_added')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_was_added', N'был добавлен', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'suggestion_panel_this_answer_was_given')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_this_answer_was_given', N'This answer was given', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_this_answer_was_given')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_this_answer_was_given', N'Этот ответ был дан', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'times_lower')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'times_lower', N'times', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'times_lower')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'times_lower', N'раз', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'suggestion_panel_extended_opinion')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_extended_opinion',
		N'Would you like to add an extended opinion?', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_extended_opinion')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_extended_opinion',
		N'Хотите рассказать поподробнее?', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'you_are_adding_detailed_description')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'you_are_adding_detailed_description',
		N'You are adding a detailed description for your answer.', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'you_are_adding_detailed_description')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'you_are_adding_detailed_description',
		N'Вы добавляете детальное описание Вашего ответа.', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'add_capital')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'add_capital',
		N'Add', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'add_capital')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'add_capital',
		N'Добавить', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'add_description')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'add_description',
		N'Add Description', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'add_description')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'add_description',
		N'Добавить Описание', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'answer_details')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'answer_details',
		N'Answer Details', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'answer_details')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'answer_details',
		N'Подробности Ответа', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'flag_lower')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'flag_lower', N'flag', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'flag_lower')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'flag_lower', N'пожаловаться', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'flag_upper')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'flag_upper', N'Flag', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'flag_upper')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'flag_upper', N'Пожаловаться', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'title_upper')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'title_upper', N'Title', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'title_upper')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'title_upper', N'Название', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'link_upper')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'link_upper', N'Link', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'link_upper')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'link_upper', N'Ссылка', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'price_upper')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'price_upper', N'Price', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'price_upper')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'price_upper', N'Цена', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'not_able_to_find_product')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'not_able_to_find_product',
		N'We were not able to find the product that best matches this answer.', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'not_able_to_find_product')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'not_able_to_find_product',
		N'К сожелению, мы не нашли продукт похожий на этот ответ.', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'vote_lower')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'vote_lower', N'vote', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'vote_lower')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'vote_lower', N'голосовать', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'vote_upper')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'vote_upper', N'Vote', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'vote_upper')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'vote_upper', N'Голосовать', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'describe_lower')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'describe_lower', N'describe', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'describe_lower')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'describe_lower', N'добавить', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'describe_upper')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'describe_upper', N'Describe', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'describe_upper')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'describe_upper', N'Добавить', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'more_lower')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'more_lower', N'more', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'more_lower')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'more_lower', N'далее', getDate());
GO

if not exists(select * from ResourceString where CultureName = 'en-US' and [Key] = 'more_upper')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('en-US', 'more_upper', N'More', getDate());
if not exists(select * from ResourceString where CultureName = 'ru-RU' and [Key] = 'more_upper')
	insert ResourceString(CultureName, [Key], Value, DateAdded) values('ru-RU', 'more_upper', N'Далее', getDate());
GO



--select * from ResourceString order by id desc
--GO

---- delete ResourceString
