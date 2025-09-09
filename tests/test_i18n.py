from game import i18n


def test_i18n_english():
    assert i18n.t("greeting", "en") == "Hello"
    assert i18n.t("farewell", "en") == "Goodbye"


def test_i18n_chinese():
    assert i18n.t("greeting", "zh") == "你好"
    assert i18n.t("farewell", "zh") == "再见"
