select top 10 'http://www.completeopinion.com/Best-' + 
replace(leftWord, ' ', '-') + '-for-' + 
replace(rightWord, ' ', '-') + '-is-' + 
replace(phrase, ' ', '-') url, id, leftWord, rightword, phrase, id, * from answers order by 2 desc

--http://www.completeopinion.com/en-US/Best-gift-for-guy-is-breathalyzer
--http://www.completeopinion.com/best-gift-for-guy-is-breathalyzer
--http://www.completeopinion.com/Best-gift-for-guy-is-breathalyzer

select  'http://www.completeopinion.com/Best-' + 
replace(leftWord, ' ', '-') + '-for-' + 
replace(rightWord, ' ', '-') + '-is-' + 
replace(phrase, ' ', '-') url, id, leftWord, rightword, phrase, id, * from answers order by 2 desc

select  'http://www.completeopinion.com/Best-' + 
replace(leftWord, ' ', '-') + '-for-' + 
replace(rightWord, ' ', '-') + '-is-' + 
replace(phrase, ' ', '-') url, id, leftWord, rightword, phrase, id, * from answers where id = 4 order by 2 desc



select len(description), * from AnswerDescriptions
