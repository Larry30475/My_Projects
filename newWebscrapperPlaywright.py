from playwright.sync_api import sync_playwright
import time
import pandas as pd
import pyperclip

country = "United States"
category = "Pet Supplies"
contact = "Email"
followers = "50K-500k"

def main():
    with sync_playwright() as p:
        browser = p.firefox.launch(headless=False)  # Set headless=True to run in the background
        context = browser.new_context()
        page = context.new_page()

        # Step 1: Open the website and log in
        page.goto("https://www.shoplus.net/login")
        page.fill("xpath=/html/body/div[3]/div/div[2]/form/div[2]/div/div/input", "marasymov@gmail.com")
        page.fill("xpath=/html/body/div[3]/div/div[2]/form/div[3]/div/div/input", "30122002Her")
        page.click("xpath=/html/body/div[3]/div/div[2]/form/div[6]/div/button")
        
        time.sleep(4)

        # Step 2: Navigate to influencers and top sales
        page.click("xpath=/html/body/div[3]/div/div[3]/div[1]/div[1]/div[1]/div/ul/li[4]/div/div[2]")
        time.sleep(1)
        page.click("xpath=/html/body/div[3]/div/div[3]/div[1]/div[1]/div[1]/div/ul/li[4]/ul/li[1]/div[2]/span")
        time.sleep(4)

        # Step 3: Select country and category
        page.click(f"xpath=//*[contains(text(), '{country}')]")
        page.click("xpath=/html/body/div[3]/div/div[3]/div[2]/div[1]/div/div/section/div/div[1]/div[2]/div/div[1]/div[2]/div/div[2]/div[2]/div/div[2]")
        time.sleep(1)
        page.click(f"xpath=//*[contains(text(), '{category}')]")

        # Handle dropdowns
        page.click("xpath=/html/body/div[3]/div/div[3]/div[2]/div[1]/div/div/section/div/div[1]/div[2]/div/div[1]/div[2]/div/div[3]/div[2]/div/div/div[2]/div[4]/div/div/div[1]/div[1]/input")
        page.click(f"xpath=//*[contains(text(), '{contact}')]")
        time.sleep(1)

        page.click("xpath=/html/body/div[3]/div/div[3]/div[2]/div[1]/div/div/section/div/div[1]/div[2]/div/div[1]/div[2]/div/div[3]/div[2]/div/div/div[2]/div[3]/div/div/div[1]/div/input")
        page.click(f"xpath=//*[contains(text(), '{followers}')]")
        time.sleep(1)

        # Click on the monthly button
        page.click("xpath=/html/body/div[3]/div/div[3]/div[2]/div[1]/div/div/section/div/div[1]/div[2]/div/div[1]/div[2]/div/div[4]/div[2]/div/div[2]/div/div/div/div/div/div[1]/a[3]")
        time.sleep(1)

        page.click("xpath=/html/body/div[3]/div/div[3]/div[2]/div[1]/div/div/section/div/div[1]/div[2]/div/div[2]/div/div/div[1]/div[2]/div[1]/div[1]/table/thead/tr/th[7]/div/span[2]/i[2]")
        time.sleep(7)

        # Step 4: Scroll to the table and get results
        table = page.locator("xpath=/html/body/div[3]/div/div[3]/div[2]/div[1]/div/div/section/div/div[2]/div[2]/div/div[2]/div/div/div[1]/div[2]/div[1]/div[2]/table")
        table.scroll_into_view_if_needed()

        results = page.locator("xpath=/html/body/div[3]/div/div[3]/div[2]/div[1]/div/div/section/div/div[2]/div[2]/div/div[2]/div/div/div[1]/div[2]/div[1]/div[2]/table/tbody/tr")

        output_file = "output.xlsx"
        df_empty = pd.DataFrame(columns=["Data"])
        with pd.ExcelWriter(output_file, engine="openpyxl") as writer:
            df_empty.to_excel(writer, index=False, sheet_name="Sheet1")

        main_tab = page

        i = 0

        while True:
            result = results.nth(i)
            result.scroll_into_view_if_needed()
            
            with context.expect_page() as new_page_info:
                result.click()
    
            new_tab = new_page_info.value
            new_tab.bring_to_front()

            # Copy data to clipboard
            new_tab.click("xpath=/html/body/div[3]/div/div[3]/div[2]/div[1]/div/div/section/div/div[1]/div[2]/main/div[1]/div/div/div[1]/div[2]/div[7]/a")
            time.sleep(2)

            # Get the copied data
            data = pyperclip.paste()

            df = pd.DataFrame([[data]], columns=["Data"])
            with pd.ExcelWriter(output_file, engine="openpyxl", mode="a", if_sheet_exists="overlay") as writer:
                df.to_excel(writer, index=False, header=False, startrow=writer.sheets["Sheet1"].max_row)

            print(f"Data saved to Excel: {data}")

            # Close the new tab and return to the main tab
            new_tab.close()
            main_tab.bring_to_front()
            time.sleep(2)

            i += 1

        print("Data successfully copied and saved to output.xlsx!")
        browser.close()

if __name__ == "__main__":
    main()