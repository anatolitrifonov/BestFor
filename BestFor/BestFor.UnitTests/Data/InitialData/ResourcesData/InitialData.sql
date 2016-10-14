if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'trending_today')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'trending_today', N'Trending Today', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'trending_today')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'trending_today', N'Популярные сегодня', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'trending_overall')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'trending_overall', N'Trending Overall', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'trending_overall')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'trending_overall', N'Популярные за все время', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'suggestion_panel_initial_message')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_initial_message', N'Enter the first word', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_initial_message')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_initial_message', N'Добавьте первое слово', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'suggestion_panel_no_answers_found')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_no_answers_found', N'No opinions found. Be the first!', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_no_answers_found')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_no_answers_found', N'Мнений не найдено. Будьте первым!', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'suggestion_panel_x_answers_found')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_x_answers_found',
		N'opinion(s) found. Click on opinion then "Add" to agree. Click the number to see additional details or vote. ' + 
			'Or feel free to type your own answer in the box above and click "Add".', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_x_answers_found')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_x_answers_found',
		N'мнений. Добавьте своё или проголосуйте за уже существующее?', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'suggestion_panel_error_happened_searching_answers')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_error_happened_searching_answers',
		N'Error happened while searching for opinions', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_error_happened_searching_answers')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_error_happened_searching_answers',
		N'Ошибка при поиске мнений', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'suggestion_panel_you_were_the_first')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_you_were_the_first',
		N'You were the first to say that', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_you_were_the_first')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_you_were_the_first',
		N'Вы были первым, сказавшим ', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'best_start_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'best_start_capital', N'Best', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'best_start_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'best_start_capital', N'Лучший', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'for_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'for_lower', N'for', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'for_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'for_lower', N'для', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'is_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'is_lower', N'is', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'is_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'is_lower', N'это', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'suggestion_panel_your_answer')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_your_answer', N'Your opinion', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_your_answer')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_your_answer', N'Ваше мнение', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'suggestion_panel_was_added')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_was_added', N'was added', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_was_added')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_was_added', N'было добавлено', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'suggestion_panel_this_answer_was_given')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_this_answer_was_given', N'This opinion was given', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_this_answer_was_given')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_this_answer_was_given', N'Это мнение было дано', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'times_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'times_lower', N'times', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'times_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'times_lower', N'раз', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'suggestion_panel_extended_opinion')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestion_panel_extended_opinion',
		N'Would you like to add an extended opinion?', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'suggestion_panel_extended_opinion')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestion_panel_extended_opinion',
		N'Хотите рассказать поподробнее?', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'you_are_adding_detailed_description')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'you_are_adding_detailed_description',
		N'You are adding a detailed description for opinion. Try to make it useful. Members will vote for it.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'you_are_adding_detailed_description')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'you_are_adding_detailed_description',
		N'Вы добавляете детальное описание мнения. Постарайтесь сделать его полезным, чтобы набрать больше голосов.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'you_are_editing_your_answer')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'you_are_editing_your_answer',
		N'You are editing your opinion.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'you_are_editing_your_answer')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'you_are_editing_your_answer',
		N'Вы редактируете Ваше мнение.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'edit_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'edit_capital',
		N'Edit', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'edit_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'edit_capital',
		N'Редактировать', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'save_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'save_capital',
		N'Save', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'save_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'save_capital',
		N'Сохранить', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'send_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'send_capital',
		N'Send', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'send_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'send_capital',
		N'Отправить', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'add_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'add_capital',
		N'Add', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'add_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'add_capital',
		N'Добавить', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'add_description')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'add_description',
		N'Add Description', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'add_description')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'add_description',
		N'Добавить Описание', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'add_your_description')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'add_your_description',
		N'Add Your Description', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'add_your_description')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'add_your_description',
		N'Добавить Ваше Описание', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'answer_details')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'answer_details',
		N'Opinion Details', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'answer_details')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'answer_details',
		N'Подробности Мнения', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'flag_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'flag_lower', N'flag', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'flag_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'flag_lower', N'пожаловаться', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'flag_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'flag_upper', N'Flag', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'flag_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'flag_upper', N'Пожаловаться', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'title_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'title_upper', N'Title', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'title_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'title_upper', N'Название', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'link_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'link_upper', N'Link', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'link_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'link_upper', N'Ссылка', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'price_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'price_upper', N'Price', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'price_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'price_upper', N'Цена', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'not_able_to_find_product')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'not_able_to_find_product',
		N'We were not able to find the product that best matches this opinion.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'not_able_to_find_product')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'not_able_to_find_product',
		N'К сожелению, мы не нашли продукт похожий на это мнение.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'vote_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'vote_lower', N'vote', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'vote_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'vote_lower', N'голосовать', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'vote_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'vote_upper', N'Vote', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'vote_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'vote_upper', N'Голосовать', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'describe_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'describe_lower', N'describe', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'describe_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'describe_lower', N'добавить', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'describe_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'describe_upper', N'Describe', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'describe_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'describe_upper', N'Добавить', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'more_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'more_lower', N'more', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'more_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'more_lower', N'далее', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'more_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'more_upper', N'More', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'more_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'more_upper', N'Далее', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'no_description_or_reasoning')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'no_description_or_reasoning',
		N'No description or reasoning was given to this opinion. Would you like to add one?', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'no_description_or_reasoning')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'no_description_or_reasoning',
		N'Никто не дал дополнительного описания для этого мнения. Хотите добавить?', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'voted_for_this_opinion')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'voted_for_this_opinion',
		N'people voted for this opinion', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'voted_for_this_opinion')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'voted_for_this_opinion',
		N'людей проголосовали за это мнение', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'found_useful_product')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'found_useful_product',
		N'We found an item that matches this opinion!', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'found_useful_product')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'found_useful_product',
		N'Эта ссылка может вас заинтересовать!', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'contact_us')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'contact_us',
		N'Contact Us', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'contact_us')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'contact_us',
		N'клщыывдал', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'suggestions_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'suggestions_upper',
		N'Suggestions', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'suggestions_upper')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'suggestions_upper',
		N'ываываыав', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'contact_us_page_text')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'contact_us_page_text',
		N'We are always glad to hear from our users! Let us know how we can help you!', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'contact_us_page_text')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'contact_us_page_text',
		N'ываываыав', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'description_was_added_successfully')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'description_was_added_successfully',
		N'Description was added successfully.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'description_was_added_successfully')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'description_was_added_successfully',
		N'Описание добавлено успешно.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'thank_you_for_voting')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'thank_you_for_voting',
		N'Thank you for voting!', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'thank_you_for_voting')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'thank_you_for_voting',
		N'Спасибо за Ваш Голос!', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'thank_you_for_flaging')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'thank_you_for_flaging',
		N'Thank you for flaging! We will check it.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'thank_you_for_flaging')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'thank_you_for_flaging',
		N'Спасибо за пометку! Будет проверено.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'improve_your_answer')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'improve_your_answer',
		N'Improve your opinion.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'improve_your_answer')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'improve_your_answer',
		N'Улучшите Ваше мнение.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'pick_category')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'pick_category',
		N'Pick Category.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'pick_category')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'pick_category',
		N'Выберите Категорию.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'none_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'none_capital',
		N'None.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'none_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'none_capital',
		N'Не установлено.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'thank_you_for_improving')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'thank_you_for_improving',
		N'Thank you for improving your opinion.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'thank_you_for_improving')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'thank_you_for_improving',
		N'Спасибо за улучшение мнения.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'thank_you_for_contacting')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'thank_you_for_contacting',
		N'Thank you for contacting us. We will get back to you as soon as we can.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'thank_you_for_contacting')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'thank_you_for_contacting',
		N'Спасибо за контакт. Обязательно прочтем.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'site_description')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'site_description',
		N'Impress the world with your opinion about the best stuff on Earth! Join the fun today! www.completeopinion.com', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'site_description')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'site_description',
		N'Поразите мир своим мнением о лучших вещах на Земле! Присоединяйтесь сегодня! www.completeopinion.com', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'global_site_index_title')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'global_site_index_title',
		N'Opinions on the best stuff on Earth!', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'global_site_index_title')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'global_site_index_title',
		N'Лучшие мнения на Земле!', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'your_profile')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'your_profile',
		N'Your profile.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'your_profile')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'your_profile',
		N'Ваш профиль.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'cancel_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'cancel_capital',
		N'Cancel', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'cancel_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'cancel_capital',
		N'Отменить', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'terms_of_use')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'terms_of_use',
		N'Anything you write and post stays with us.

Your username and display name cannot be reused.

If you cancel the account, we will delete all your personal information except for username and display name. Your opinions and descriptions will be marked as Anonymous.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'terms_of_use')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'terms_of_use',
		N'Anything you write and post stays with us.

Your username and display name cannot be reused.

If you cancel the account, we will delete all your personal information except for username and display name. Your opinions and descriptions will be marked as Anonymous.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'terms_title')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'terms_title',
		N'Terms', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'terms_title')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'terms_title',
		N'Соглашение', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'register_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'register_capital',
		N'Register', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'register_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'register_capital',
		N'Зарегистрироваться', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'registration_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'registration_capital',
		N'Registration', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'registration_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'registration_capital',
		N'Зарегистрация', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'create_a_new_account')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'create_a_new_account',
		N'Create a new account', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'create_a_new_account')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'create_a_new_account',
		N'Создать учетную запись', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'edit_profile')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'edit_profile',
		N'Edit profile', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'edit_profile')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'edit_profile',
		N'Ваш профиль', getDate());
GO


if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'remove_profile')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'remove_profile',
		N'Remove profile', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'remove_profile')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'remove_profile',
		N'Ваш профиль', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'are_you_sure_you_want_to_remove_profile')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'are_you_sure_you_want_to_remove_profile',
		N'Are you sure you want to remove your profile?

Please tell us why.

Upon account removal we will delete all your personal information and mark all your opinions and descriptions as anonymous.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'are_you_sure_you_want_to_remove_profile')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'are_you_sure_you_want_to_remove_profile',
		N'Ваш профиль', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'your_profile_was_removed')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'your_profile_was_removed',
		N'Your profile was removed', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'your_profile_was_removed')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'your_profile_was_removed',
		N'Ваш профиль был удален', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'error_page_general_message')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'error_page_general_message',
		N'An error occurred while processing your request.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'error_page_general_message')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'error_page_general_message',
		N'Произошла ошибка.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'error_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'error_capital',
		N'Error', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'error_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'error_capital',
		N'Ошибка', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'error_bad_characters')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'error_bad_characters',
		N'Some strange characters where found in the data you posted.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'error_bad_characters')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'error_bad_characters',
		N'Вы отправили нам странные символы.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'error_profanity')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'error_profanity',
		N'Profanity', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'error_profanity')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'error_profanity',
		N'Не разрешено', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'search_results_for')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'search_results_for',
		N'Search results', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'search_results_for')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'search_results_for',
		N'Результаты поиска', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'help_others')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'help_others',
		N'Help others.', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'help_others')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'help_others',
		N'Помогите другим.', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'add_your_opinion')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'add_your_opinion',
		N'Add your opinion!', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'add_your_opinion')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'add_your_opinion',
		N'Добавьте Ваше мнение!', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'search_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'search_capital',
		N'Search', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'search_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'search_capital',
		N'Искать', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'everything_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'everything_capital',
		N'Everything', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'everything_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'everything_capital',
		N'Все', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'last_100')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'last_100',
		N'Last 100', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'last_100')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'last_100',
		N'Последние 100', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'search_for_opinion')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'search_for_opinion', N'Search for opinion ...', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'search_for_opinion')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'search_for_opinion', N'Искать мнение ...', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'contribute_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'contribute_capital', N'Contribute', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'contribute_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'contribute_capital', N'Добавить', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'poster_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'poster_lower', N'apinioner', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'poster_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'poster_lower', N'мнитель', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'poster_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'poster_capital', N'Apinioner', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'poster_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'poster_capital', N'Мнитель', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'posters_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'posters_lower', N'apinioners', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'posters_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'posters_lower', N'мнители', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'posters_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'posters_capital', N'Apinioners', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'posters_capital')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'posters_capital', N'Мнители', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'top_posters')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'top_posters', N'Top {{posters_capital}}', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'top_posters')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'top_posters', N'Топ {{posters_capital}}', getDate());
GO

if not exists(select * from ResourceStrings where CultureName = 'en-US' and [Key] = 'opinions_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('en-US', 'opinions_lower', N'opinions', getDate());
if not exists(select * from ResourceStrings where CultureName = 'ru-RU' and [Key] = 'opinions_lower')
	insert ResourceStrings(CultureName, [Key], Value, DateAdded) values('ru-RU', 'opinions_lower', N'мнений', getDate());
GO


-- delete from ResourceStrings where [key] in ('help_others', 'add_your_opinion', 'if_search_is_not_sufficient')